using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace LukSDK.Utils
{
    public static class SignatureUtils
    {
        public static string GenerateSignature(string secret, object parameters)
        {
            if (string.IsNullOrEmpty(secret)) throw new ArgumentException("Secret is required", nameof(secret));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            // Use the runtime type so derived request wire fields are included.
            using var doc = JsonDocument.Parse(JsonSerializer.Serialize(parameters, parameters.GetType(), LukSDKJson.CreateOptions()));
            var values = new SortedDictionary<string, string>(StringComparer.Ordinal);
            foreach (var p in doc.RootElement.EnumerateObject())
            {
                if (p.Name == "sign") continue;
                switch (p.Value.ValueKind)
                {
                    case JsonValueKind.String:
                        var text = p.Value.GetString();
                        if (!string.IsNullOrEmpty(text)) values[p.Name] = text!;
                        break;
                    case JsonValueKind.Number:
                        if (p.Value.TryGetInt64(out var integer))
                        {
                            if (integer != 0) values[p.Name] = integer.ToString(CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            var number = p.Value.GetDouble();
                            if (number != 0) values[p.Name] = number.ToString("G", CultureInfo.InvariantCulture);
                        }
                        break;
                    case JsonValueKind.True: values[p.Name] = "true"; break;
                }
            }
            var input = string.Join("&", values.Select(p => p.Key + "=" + p.Value)) + "&key=" + secret;
            using var md5 = MD5.Create();
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-", "");
        }

        public static bool VerifySignature(string secret, object parameters, string signature)
        {
            if (string.IsNullOrEmpty(signature)) return false;
            return string.Equals(GenerateSignature(secret, parameters), signature, StringComparison.OrdinalIgnoreCase);
        }
    }
}
