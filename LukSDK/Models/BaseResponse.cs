using System.Text.Json.Serialization;

namespace LukSDK.Models
{
    /// <summary>
    /// API 响应基类
    /// </summary>
    public abstract class BaseResponse
    {
        /// <summary>
        /// 响应代码
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 判断响应是否成功
        /// </summary>
        /// <returns>成功返回 true，否则返回 false</returns>
        public virtual bool IsSuccess => Code == 0;

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