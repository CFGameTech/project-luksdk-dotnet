using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LukSDK.Models.Callbacks
{
    public class CreateChannelOrderRequest
    {
        [JsonPropertyName("data")]
        public List<CreateChannelOrderOrder>? Data { get; set; }
        [JsonPropertyName("nonce")]
        public string? Nonce { get; set; }
        [JsonPropertyName("timestamp")]
        public long? Timestamp { get; set; }
        [JsonPropertyName("sign")]
        public string? Sign { get; set; }
    }
    public class CreateChannelOrderOrder
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
        [JsonPropertyName("c_room_id")]
        public string? RoomId { get; set; }
        [JsonPropertyName("item_id")]
        public string? ItemId { get; set; }
        [JsonPropertyName("num")]
        public long? Num { get; set; }
        [JsonPropertyName("ext")]
        public string? Ext { get; set; }
    }
    public class CreateChannelOrderResponse : BaseResponse
    {
        [JsonPropertyName("data")]
        public List<CreateChannelOrderResult>? Data { get; set; }
    }
    public class CreateChannelOrderResult
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
        [JsonPropertyName("status")]
        public int? Status { get; set; }
    }
}
