using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LukSDK.Models.Callbacks
{
    public class NotifyEventRequest
    {
        [JsonPropertyName("app_id")]
        public long? AppId { get; set; }
        [JsonPropertyName("game_id")]
        public long? GameId { get; set; }
        [JsonPropertyName("room_id")]
        public string? RoomId { get; set; }
        [JsonPropertyName("round_id")]
        public string? RoundId { get; set; }
        [JsonPropertyName("timestamp")]
        public long? Timestamp { get; set; }
        [JsonPropertyName("sign")]
        public string? Sign { get; set; }
        [JsonPropertyName("type")]
        public int Type { get; set; }
        [JsonPropertyName("data")]
        public string? Data { get; set; }
    }
    public class NotifyEventResponse : BaseResponse { }
    public class NotifyEventStartGame
    {
        [JsonPropertyName("start_unix_sec")]
        public long? StartUnixSec { get; set; }
        [JsonPropertyName("user_ids")]
        public List<string>? UserIds { get; set; }
        [JsonPropertyName("op_user_id")]
        public string? OpUserId { get; set; }
        [JsonPropertyName("start_ext")]
        public string? StartExt { get; set; }
    }
    public class NotifyEventGameOverUserResult
    {
        [JsonPropertyName("score")]
        public long? Score { get; set; }
        [JsonPropertyName("rank")]
        public int? Rank { get; set; }
        [JsonPropertyName("status")]
        public int? Status { get; set; }
        [JsonPropertyName("escape")]
        public bool? Escape { get; set; }
        [JsonPropertyName("trust")]
        public bool? Trust { get; set; }
    }
    public class NotifyEventGameOver
    {
        [JsonPropertyName("start_unix_sec")]
        public long? StartUnixSec { get; set; }
        [JsonPropertyName("end_unix_sec")]
        public long? EndUnixSec { get; set; }
        [JsonPropertyName("user_results")]
        public Dictionary<string, NotifyEventGameOverUserResult>? UserResults { get; set; }
        [JsonPropertyName("end_type")]
        public int? EndType { get; set; }
        [JsonPropertyName("end_ext")]
        public string? EndExt { get; set; }
        [JsonPropertyName("cost_coins")]
        public long? CostCoins { get; set; }
        [JsonPropertyName("op_user_id")]
        public string? OpUserId { get; set; }
        [JsonPropertyName("start_ext")]
        public string? StartExt { get; set; }
    }
    public class NotifyEventUserChange
    {
        [JsonPropertyName("user_ids")]
        public List<string>? UserIds { get; set; }
        [JsonPropertyName("type")]
        public int? Type { get; set; }
        [JsonPropertyName("player_state")]
        public Dictionary<string, bool>? PlayerState { get; set; }
        [JsonPropertyName("ob_user_ids")]
        public List<string>? ObUserIds { get; set; }
        [JsonPropertyName("gaming")]
        public bool? Gaming { get; set; }
    }
    public class NotifyEventRoomSettingChange
    {
        [JsonPropertyName("op_user_id")]
        public string? OpUserId { get; set; }
        [JsonPropertyName("setting")]
        public string? Setting { get; set; }
    }
    public class NotifyEventSeatSync
    {
        [JsonPropertyName("players")]
        public List<NotifyEventSeatPlayer>? Players { get; set; }
        [JsonPropertyName("seat_user_ids")]
        public Dictionary<int, string>? SeatUserIds { get; set; }
    }
    public class NotifyEventPropEquip
    {
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }
        [JsonPropertyName("equipped_prop_id")]
        public string? EquippedPropId { get; set; }
        [JsonPropertyName("unequipped_prop_id")]
        public string? UnequippedPropId { get; set; }
    }
    public class NotifyEventSeatPlayer
    {
        [JsonPropertyName("user_id")] public string? UserId { get; set; }
        [JsonPropertyName("seat")] public int? Seat { get; set; }
        [JsonPropertyName("is_prepared")] public bool? IsPrepared { get; set; }
        [JsonPropertyName("is_robot")] public bool? IsRobot { get; set; }
        [JsonPropertyName("robot_level")] public int? RobotLevel { get; set; }
    }
}
