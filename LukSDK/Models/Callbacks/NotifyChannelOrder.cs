using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LukSDK.Models.Callbacks
{
    public class NotifyChannelOrderRequest
    {
        [JsonPropertyName("data")]
        public List<NotifyChannelOrderOrder>? Data { get; set; }
        [JsonPropertyName("nonce")]
        public string? Nonce { get; set; }
        [JsonPropertyName("timestamp")]
        public long? Timestamp { get; set; }
        [JsonPropertyName("sign")]
        public string? Sign { get; set; }
    }
    public class NotifyChannelOrderOrder
    {
        [JsonPropertyName("c_id")]
        public long? AppId { get; set; }
        [JsonPropertyName("g_id")]
        public long? GameId { get; set; }
        [JsonPropertyName("c_uid")]
        public string? UserId { get; set; }
        [JsonPropertyName("token")]
        public string? Token { get; set; }
        [JsonPropertyName("game_order_id")]
        public string? GameOrderId { get; set; }
        [JsonPropertyName("coins_cost")]
        public long? CoinsCost { get; set; }
        [System.Obsolete("已废弃，不再使用；仅保留兼容。")]
        [JsonPropertyName("score_cost")]
        public long? ScoreCost { get; set; }
        [JsonPropertyName("timestamp")]
        public long? Timestamp { get; set; }
        [JsonPropertyName("coins_award")]
        public long? CoinsAward { get; set; }
        [System.Obsolete("已废弃，不再使用；仅保留兼容。")]
        [JsonPropertyName("score_award")]
        public long? ScoreAward { get; set; }
        [JsonPropertyName("status")]
        public int? Status { get; set; }
    }
    public class NotifyChannelOrderResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public List<NotifyChannelOrderResult>? Data { get; set; }
    }
    public class NotifyChannelOrderResult
    {
        [JsonPropertyName("c_uid")]
        public string? UserId { get; set; }
        [JsonPropertyName("coins")]
        public long? Coins { get; set; }
        [System.Obsolete("已废弃，不再使用；仅保留兼容。")]
        [JsonPropertyName("score")]
        public long? Score { get; set; }
        [JsonPropertyName("order_id")]
        public string? OrderId { get; set; }
    }
}
