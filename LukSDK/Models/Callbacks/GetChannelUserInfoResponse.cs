using System.Text.Json.Serialization;

namespace LukSDK.Models.Callbacks
{
    /// <summary>
    /// 获取渠道用户信息响应
    /// </summary>
    public class GetChannelUserInfoResponse
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
        public UserData? Data { get; set; }

        /// <summary>
        /// 判断响应是否成功
        /// </summary>
        /// <returns>成功返回 true，否则返回 false</returns>
        public bool IsSuccess => Code == 0;

        /// <summary>
        /// 用户数据类
        /// </summary>
        public class UserData
        {
            /// <summary>
            /// 用户昵称
            /// </summary>
            [JsonPropertyName("nickname")]
            public string? Nickname { get; set; }

            /// <summary>
            /// 用户头像
            /// </summary>
            [JsonPropertyName("avatar")]
            public string? Avatar { get; set; }

            /// <summary>
            /// 用户金币
            /// </summary>
            [JsonPropertyName("coins")]
            public long? Coins { get; set; }

            /// <summary>
            /// 用户身份
            /// </summary>
            [JsonPropertyName("identity")]
            public int? Identity { get; set; }

            /// <summary>
            /// 返回用户数据的字符串表示
            /// </summary>
            /// <returns>格式化的用户数据</returns>
            public override string ToString()
            {
                return $"UserData[Nickname={Nickname}, Avatar={Avatar}, Coins={Coins}, Identity={Identity}]";
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