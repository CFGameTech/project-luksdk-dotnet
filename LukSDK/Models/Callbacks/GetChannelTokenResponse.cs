using System.Text.Json.Serialization;

namespace LukSDK.Models.Callbacks
{
    /// <summary>
    /// 获取渠道令牌响应
    /// </summary>
    public class GetChannelTokenResponse
    {
        /// <summary>
        /// 请求状态码，当值为 0 时表示请求成功
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        /// 请求状态说明
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 响应数据
        /// </summary>
        [JsonPropertyName("data")]
        public ResponseData? Data { get; set; }

        /// <summary>
        /// 判断响应是否成功
        /// </summary>
        /// <returns>成功返回 true，否则返回 false</returns>
        public bool IsSuccess => Code == 0;

        /// <summary>
        /// 响应数据类
        /// </summary>
        public class ResponseData
        {
            /// <summary>
            /// 用户令牌
            /// </summary>
            [JsonPropertyName("token")]
            public string? Token { get; set; }

            /// <summary>
            /// 剩余秒数
            /// </summary>
            [JsonPropertyName("left_time")]
            public long LeftTime { get; set; }

            /// <summary>
            /// 返回响应数据的字符串表示
            /// </summary>
            /// <returns>格式化的响应数据</returns>
            public override string ToString()
            {
                return $"ResponseData[Token={Token}, LeftTime={LeftTime}]";
            }
        }

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