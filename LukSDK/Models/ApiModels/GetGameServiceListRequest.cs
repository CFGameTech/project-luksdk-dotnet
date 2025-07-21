using System.Text.Json.Serialization;

namespace LukSDK.Models.ApiModels
{
    /// <summary>
    /// 获取游戏服务列表请求
    /// </summary>
    public class GetGameServiceListRequest : BaseRequest
    {
        /// <summary>
        /// 应用 ID
        /// </summary>
        [JsonPropertyName("c_id")]
        public new long? AppId { get; set; }

        /// <summary>
        /// 初始化 GetGameServiceListRequest 实例
        /// </summary>
        public GetGameServiceListRequest()
        {
        }

        /// <summary>
        /// 初始化 GetGameServiceListRequest 实例
        /// </summary>
        /// <param name="appId">应用 ID</param>
        public GetGameServiceListRequest(long appId)
        {
            AppId = appId;
        }
    }
}