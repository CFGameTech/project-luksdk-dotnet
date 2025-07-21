using System.Text.Json.Serialization;

namespace LukSDK.Models.ApiModels
{
    /// <summary>
    /// 查询通知事件请求
    /// </summary>
    public class QueryNotifyEventRequest : BaseRequest
    {
        /// <summary>
        /// 应用 ID
        /// </summary>
        [JsonPropertyName("app_id")]
        public new long? AppId { get; set; }

        /// <summary>
        /// 游戏 ID（必填）
        /// </summary>
        [JsonPropertyName("game_id")]
        public long GameId { get; set; }

        /// <summary>
        /// 房间 ID
        /// </summary>
        [JsonPropertyName("room_id")]
        public string? RoomId { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        [JsonPropertyName("type")]
        public long? Type { get; set; }

        /// <summary>
        /// 开始时间（时间戳）
        /// </summary>
        [JsonPropertyName("start_at")]
        public long? StartAt { get; set; }

        /// <summary>
        /// 结束时间（时间戳）
        /// </summary>
        [JsonPropertyName("end_at")]
        public long? EndAt { get; set; }

        /// <summary>
        /// 页码（必填）
        /// </summary>
        [JsonPropertyName("page_no")]
        public long PageNo { get; set; }

        /// <summary>
        /// 页大小（必填）
        /// </summary>
        [JsonPropertyName("page_size")]
        public long PageSize { get; set; }

        /// <summary>
        /// 初始化 QueryNotifyEventRequest 实例
        /// </summary>
        public QueryNotifyEventRequest()
        {
        }

        /// <summary>
        /// 初始化 QueryNotifyEventRequest 实例
        /// </summary>
        /// <param name="gameId">游戏 ID</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页大小</param>
        public QueryNotifyEventRequest(long gameId, long pageNo, long pageSize)
        {
            GameId = gameId;
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}