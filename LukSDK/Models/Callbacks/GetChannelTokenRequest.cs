using System.Text.Json.Serialization;

namespace LukSDK.Models.Callbacks
{
    /// <summary>
    /// 获取渠道令牌请求
    /// </summary>
    public class GetChannelTokenRequest
    {
        /// <summary>
        /// 渠道 ID
        /// </summary>
        [JsonPropertyName("c_id")]
        public long? AppId { get; set; }

        /// <summary>
        /// 渠道用户 ID
        /// </summary>
        [JsonPropertyName("c_uid")]
        public string? UserId { get; set; }

        /// <summary>
        /// 渠道用户临时 code
        /// </summary>
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        /// <summary>
        /// 秒级时间戳
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
                return System.Text.Json.JsonSerializer.Serialize(this, CreateJsonSerializerOptions());
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
        private static System.Text.Json.JsonSerializerOptions CreateJsonSerializerOptions()
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