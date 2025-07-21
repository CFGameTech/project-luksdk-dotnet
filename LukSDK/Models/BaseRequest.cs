using System.Text.Json.Serialization;

namespace LukSDK.Models
{
    /// <summary>
    /// API 请求基类
    /// </summary>
    public abstract class BaseRequest
    {
        /// <summary>
        /// 应用 ID（注意：不同 API 使用不同的字段名，子类需要重新定义）
        /// </summary>
        [JsonIgnore]
        public long? AppId { get; set; }

        /// <summary>
        /// 时间戳（秒级）
        /// </summary>
        [JsonPropertyName("timestamp")]
        public long? Timestamp { get; set; }

        /// <summary>
        /// 请求签名
        /// </summary>
        [JsonPropertyName("sign")]
        public string? Sign { get; set; }

        /// <summary>
        /// 返回对象的字符串表示
        /// </summary>
        /// <returns>JSON 格式的字符串</returns>
        public override string ToString()
        {
            try
            {
                return System.Text.Json.JsonSerializer.Serialize(this, GetType(), CreateJsonSerializerOptions());
            }
            catch
            {
                return base.ToString() ?? string.Empty;
            }
        }

        /// <summary>
        /// 创建 JSON 序列化选项
        /// </summary>
        /// <returns>配置好的 JsonSerializerOptions</returns>
        protected virtual System.Text.Json.JsonSerializerOptions CreateJsonSerializerOptions()
        {
            return new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true
            };
        }
    }
}