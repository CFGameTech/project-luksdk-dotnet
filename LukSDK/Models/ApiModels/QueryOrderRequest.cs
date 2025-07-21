using System.Text.Json.Serialization;

namespace LukSDK.Models.ApiModels
{
    /// <summary>
    /// 查询订单请求
    /// </summary>
    public class QueryOrderRequest : BaseRequest
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
        /// 渠道订单 ID（与 GameOrderNo 二选一）
        /// </summary>
        [JsonPropertyName("app_order_no")]
        public string? AppOrderNo { get; set; }

        /// <summary>
        /// 游戏订单 ID（与 AppOrderNo 二选一）
        /// </summary>
        [JsonPropertyName("game_order_no")]
        public string? GameOrderNo { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        [JsonPropertyName("nonce")]
        public string? Nonce { get; set; }

        /// <summary>
        /// 初始化 QueryOrderRequest 实例
        /// </summary>
        public QueryOrderRequest()
        {
        }

        /// <summary>
        /// 初始化 QueryOrderRequest 实例
        /// </summary>
        /// <param name="gameId">游戏 ID</param>
        /// <param name="appOrderNo">渠道订单 ID</param>
        public QueryOrderRequest(long gameId, string appOrderNo)
        {
            GameId = gameId;
            AppOrderNo = appOrderNo;
        }

        /// <summary>
        /// 创建基于游戏订单 ID 的查询请求
        /// </summary>
        /// <param name="gameId">游戏 ID</param>
        /// <param name="gameOrderNo">游戏订单 ID</param>
        /// <returns>查询订单请求实例</returns>
        public static QueryOrderRequest CreateByGameOrderNo(long gameId, string gameOrderNo)
        {
            return new QueryOrderRequest
            {
                GameId = gameId,
                GameOrderNo = gameOrderNo
            };
        }
    }
}