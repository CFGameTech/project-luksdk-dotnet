using System.Text.Json.Serialization;

namespace LukSDK.Models.ApiModels
{
    /// <summary>
    /// 查询订单响应
    /// </summary>
    public class QueryOrderResponse : BaseResponse
    {
        /// <summary>
        /// 订单数据
        /// </summary>
        [JsonPropertyName("data")]
        public OrderData? Data { get; set; }

        /// <summary>
        /// 订单数据类
        /// </summary>
        public class OrderData
        {
            /// <summary>
            /// 应用 ID
            /// </summary>
            [JsonPropertyName("app_id")]
            public long AppId { get; set; }

            /// <summary>
            /// 渠道订单 ID
            /// </summary>
            [JsonPropertyName("app_order_id")]
            public string? AppOrderId { get; set; }

            /// <summary>
            /// 游戏订单 ID
            /// </summary>
            [JsonPropertyName("game_order_id")]
            public string? GameOrderId { get; set; }

            /// <summary>
            /// 用户 ID
            /// </summary>
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }

            /// <summary>
            /// 道具 ID
            /// </summary>
            [JsonPropertyName("item_id")]
            public string? ItemId { get; set; }

            /// <summary>
            /// 数量
            /// </summary>
            [JsonPropertyName("num")]
            public long? Num { get; set; }

            /// <summary>
            /// 消耗金币
            /// </summary>
            [JsonPropertyName("coins_cost")]
            public long? CoinsCost { get; set; }

            /// <summary>
            /// 奖励金币
            /// </summary>
            [JsonPropertyName("coins_award")]
            public long? CoinsAward { get; set; }

            /// <summary>
            /// 收益
            /// </summary>
            [JsonPropertyName("gain")]
            public long? Gain { get; set; }

            /// <summary>
            /// 主播抽成
            /// </summary>
            [JsonPropertyName("anchor_draw")]
            public long? AnchorDraw { get; set; }

            /// <summary>
            /// 官方抽成金币
            /// </summary>
            [JsonPropertyName("coins_official_draw")]
            public long? CoinsOfficialDraw { get; set; }

            /// <summary>
            /// 支付状态
            /// </summary>
            [JsonPropertyName("pay_status")]
            public int PayStatus { get; set; }

            /// <summary>
            /// 游戏支付状态
            /// </summary>
            [JsonPropertyName("pay_game_status")]
            public int PayGameStatus { get; set; }

            /// <summary>
            /// 创建时间（时间戳）
            /// </summary>
            [JsonPropertyName("create_time")]
            public long CreateTime { get; set; }

            /// <summary>
            /// 扩展信息
            /// </summary>
            [JsonPropertyName("ext")]
            public string? Ext { get; set; }

            /// <summary>
            /// 返回订单数据的字符串表示
            /// </summary>
            /// <returns>格式化的订单信息</returns>
            public override string ToString()
            {
                return $"OrderData[AppOrderId={AppOrderId}, GameOrderId={GameOrderId}, UserId={UserId}, PayStatus={PayStatus}]";
            }
        }
    }
}