using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace LukSDK.Utils
{
    /// <summary>
    /// 签名工具类
    /// </summary>
    public static class SignatureUtils
    {
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="secret">密钥</param>
        /// <param name="parameters">参数对象</param>
        /// <returns>MD5 签名字符串（大写）</returns>
        public static string GenerateSignature(string secret, object parameters)
        {
            if (string.IsNullOrEmpty(secret))
                throw new ArgumentException("Secret cannot be null or empty", nameof(secret));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var signatureParams = ConvertToSignatureParams(parameters);
            return ComputeSignature(secret, signatureParams);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="secret">密钥</param>
        /// <param name="parameters">参数对象</param>
        /// <param name="signature">待验证的签名</param>
        /// <returns>签名是否有效</returns>
        public static bool VerifySignature(string secret, object parameters, string signature)
        {
            if (string.IsNullOrEmpty(signature))
                return false;

            try
            {
                var expectedSignature = GenerateSignature(secret, parameters);
                return string.Equals(expectedSignature, signature, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将对象转换为签名参数字典
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>扁平化的键值对字典</returns>
        private static Dictionary<string, string> ConvertToSignatureParams(object obj)
        {
            var result = new Dictionary<string, string>();

            try
            {
                // 使用 System.Text.Json 序列化对象为 JsonElement
                var jsonString = JsonSerializer.Serialize(obj, CreateJsonSerializerOptions());
                var jsonDocument = JsonDocument.Parse(jsonString);
                
                // 递归展平 JSON 结构
                FlattenJsonElement("", jsonDocument.RootElement, result);
                
                // 移除 sign 字段
                result.Remove("sign");
                
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to convert object to signature params", ex);
            }
        }

        /// <summary>
        /// 递归展平 JsonElement
        /// </summary>
        /// <param name="prefix">当前键前缀</param>
        /// <param name="element">JSON 元素</param>
        /// <param name="result">结果字典</param>
        private static void FlattenJsonElement(string prefix, JsonElement element, Dictionary<string, string> result)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    foreach (var property in element.EnumerateObject())
                    {
                        var key = string.IsNullOrEmpty(prefix) ? property.Name : $"{prefix}.{property.Name}";
                        FlattenJsonElement(key, property.Value, result);
                    }
                    break;

                case JsonValueKind.Array:
                    var index = 0;
                    foreach (var item in element.EnumerateArray())
                    {
                        var key = $"{prefix}[{index}]";
                        FlattenJsonElement(key, item, result);
                        index++;
                    }
                    break;

                case JsonValueKind.String:
                    result[prefix] = element.GetString() ?? string.Empty;
                    break;

                case JsonValueKind.Number:
                    result[prefix] = element.ToString();
                    break;

                case JsonValueKind.True:
                    result[prefix] = "true";
                    break;

                case JsonValueKind.False:
                    result[prefix] = "false";
                    break;

                case JsonValueKind.Null:
                    // 忽略 null 值
                    break;

                default:
                    result[prefix] = element.ToString();
                    break;
            }
        }

        /// <summary>
        /// 计算 MD5 签名
        /// </summary>
        /// <param name="secret">密钥</param>
        /// <param name="parameters">参数字典</param>
        /// <returns>MD5 签名字符串（大写）</returns>
        private static string ComputeSignature(string secret, Dictionary<string, string> parameters)
        {
            // 按键排序
            var sortedParams = parameters
                .Where(kvp => !string.IsNullOrEmpty(kvp.Value)) // 过滤空值
                .OrderBy(kvp => kvp.Key)
                .ToList();

            // 构建签名字符串
            var signatureBuilder = new StringBuilder();
            foreach (var kvp in sortedParams)
            {
                signatureBuilder.Append($"{kvp.Key}={kvp.Value}&");
            }

            // 拼接 &key=secret
            signatureBuilder.Append($"key={secret}");

            var signatureString = signatureBuilder.ToString();

            // 计算 MD5 并转大写
            return ComputeMD5Hash(signatureString);
        }

        /// <summary>
        /// 计算 MD5 哈希值
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>MD5 哈希值（大写）</returns>
        private static string ComputeMD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// 创建 JSON 序列化选项
        /// </summary>
        /// <returns>配置好的 JsonSerializerOptions</returns>
        private static JsonSerializerOptions CreateJsonSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true
            };
        }
    }
}