using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace project_luksdk_dotnet_standard_2
{
    public static class SignatureHelper
    {
        // 签名方法
        public static string Signature(string signSecret, object parameters)
        {
            var paramsMap = CastToSignatureParams(parameters);
            return GenerateSignature(signSecret, paramsMap);
        }

        // 生成签名
        private static string GenerateSignature(string signSecret, Dictionary<string, string> parameters)
        {
            // 提取并排序参数键
            var keys = parameters.Keys.ToList();
            keys.Sort();

            // 构建签名字符串
            var signatureBuilder = new StringBuilder();
            foreach (var key in keys)
            {
                var value = parameters[key];
                if (!string.IsNullOrEmpty(value))
                {
                    signatureBuilder.Append($"{key}={value}&");
                }
            }

            // 删除末尾的 '&' 并拼接密钥
            if (signatureBuilder.Length > 0)
                signatureBuilder.Length--; // 去掉最后的 "&"
            signatureBuilder.Append($"&key={signSecret}");

            // 生成 MD5 哈希并将结果转换为大写
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(signatureBuilder.ToString()));
            return BitConverter.ToString(hash).Replace("-", "").ToUpper();
        }

        // 将对象转换为 Dictionary<string, string>
        private static Dictionary<string, string> CastToSignatureParams(object obj)
        {
            var result = new Dictionary<string, string>();

            if (obj is IDictionary<string, object> dict)
            {
                foreach (var kvp in dict)
                {
                    result[kvp.Key] = kvp.Value?.ToString() ?? string.Empty;
                }
            }
            else
            {
                var type = obj.GetType();
                foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var jsonTag = prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name;
                    if (jsonTag == null || jsonTag == "sign") continue;
                    
                    // 只处理基本类型
                    if ((!prop.PropertyType.IsPrimitive && prop.PropertyType != typeof(string)) && prop.PropertyType != typeof(NotifyType) || prop.PropertyType == typeof(DateTime))
                    {
                        continue;
                    }

                    var pass = true;
                    var value = prop.GetValue(obj);
                    if (prop.PropertyType == typeof(NotifyType))
                    {
                        value = ((NotifyType)value).GetHashCode();
                    }
                    switch (prop.PropertyType)
                    {
                        case var _ when value is long l:
                            if (l == 0) pass = false;
                            break;
                        case var _ when value is int i:
                            if (i == 0) pass = false;
                            break;
                        case var _ when value is string s:
                            if (string.IsNullOrEmpty(s)) pass = false;
                            break;
                        case var _ when value is bool b:
                            if (!b) pass = false;
                            break;
                        case var _ when value is double d:
                            if (d == 0) pass = false;
                            break;    
                        case var _ when value is float f:
                            if (f == 0) pass = false;
                            break;
                    }

                    if (value == null) pass = false;
                    if (pass)
                    {
                        result[jsonTag] = value?.ToString();
                    }
                }
            }

            return result;
        }
    }
}