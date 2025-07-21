using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LukSDK.Models.ApiModels
{
    /// <summary>
    /// 发布控制事件请求
    /// </summary>
    public class PublishControlEventRequest : BaseRequest
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
        /// 事件类型（必填）
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; }

        /// <summary>
        /// 事件数据（JSON 字符串）
        /// </summary>
        [JsonPropertyName("data")]
        public string? Data { get; set; }

        /// <summary>
        /// 初始化 PublishControlEventRequest 实例
        /// </summary>
        public PublishControlEventRequest()
        {
        }

        /// <summary>
        /// 初始化 PublishControlEventRequest 实例
        /// </summary>
        /// <param name="gameId">游戏 ID</param>
        /// <param name="type">事件类型</param>
        /// <param name="data">事件数据</param>
        public PublishControlEventRequest(long gameId, int type, string? data = null)
        {
            GameId = gameId;
            Type = type;
            Data = data;
        }

        /// <summary>
        /// 初始化 PublishControlEventRequest 实例
        /// </summary>
        /// <param name="gameId">游戏 ID</param>
        /// <param name="roomId">房间 ID</param>
        /// <param name="type">事件类型</param>
        /// <param name="data">事件数据</param>
        public PublishControlEventRequest(long gameId, string roomId, int type, string? data = null)
        {
            GameId = gameId;
            RoomId = roomId;
            Type = type;
            Data = data;
        }
    }

    /// <summary>
    /// 控制事件数据模型
    /// </summary>
    public static class ControlEventModels
    {
        /// <summary>
        /// 控制事件接口
        /// </summary>
        public interface IControlEvent
        {
        }

        /// <summary>
        /// 加入游戏事件
        /// </summary>
        public class JoinGame : IControlEvent
        {
            /// <summary>
            /// 用户 ID（必填）
            /// </summary>
            [JsonPropertyName("user_id")]
            public string UserId { get; set; } = string.Empty;

            /// <summary>
            /// 座位号
            /// </summary>
            [JsonPropertyName("seat")]
            public int? Seat { get; set; }

            /// <summary>
            /// 是否准备
            /// </summary>
            [JsonPropertyName("ready")]
            public bool? Ready { get; set; }

            /// <summary>
            /// 自动开始人数
            /// </summary>
            [JsonPropertyName("auto_start_num")]
            public int? AutoStartNum { get; set; }
        }

        /// <summary>
        /// 离开游戏事件
        /// </summary>
        public class LeaveGame : IControlEvent
        {
            /// <summary>
            /// 用户 ID（必填）
            /// </summary>
            [JsonPropertyName("user_id")]
            public string UserId { get; set; } = string.Empty;
        }

        /// <summary>
        /// 改变准备状态事件
        /// </summary>
        public class ChangeReadyStatus : IControlEvent
        {
            /// <summary>
            /// 用户 ID（必填）
            /// </summary>
            [JsonPropertyName("user_id")]
            public string UserId { get; set; } = string.Empty;

            /// <summary>
            /// 是否准备
            /// </summary>
            [JsonPropertyName("is_prepare")]
            public bool? IsPrepare { get; set; }
        }

        /// <summary>
        /// 踢出玩家事件
        /// </summary>
        public class KickPlayer : IControlEvent
        {
            /// <summary>
            /// 操作用户 ID
            /// </summary>
            [JsonPropertyName("op_user_id")]
            public string? OpUserId { get; set; }

            /// <summary>
            /// 被踢用户 ID（必填）
            /// </summary>
            [JsonPropertyName("user_id")]
            public string UserId { get; set; } = string.Empty;

            /// <summary>
            /// 踢出原因
            /// </summary>
            [JsonPropertyName("reason")]
            public string? Reason { get; set; }
        }

        /// <summary>
        /// 开始游戏事件
        /// </summary>
        public class StartGame : IControlEvent
        {
            /// <summary>
            /// 操作用户 ID
            /// </summary>
            [JsonPropertyName("op_user_id")]
            public string? OpUserId { get; set; }

            /// <summary>
            /// 是否强制开始
            /// </summary>
            [JsonPropertyName("force")]
            public bool? Force { get; set; }

            /// <summary>
            /// 开始扩展信息
            /// </summary>
            [JsonPropertyName("start_ext")]
            public string? StartExt { get; set; }
        }

        /// <summary>
        /// 强制关闭游戏事件
        /// </summary>
        public class ForceCloseGame : IControlEvent
        {
            /// <summary>
            /// 操作用户 ID
            /// </summary>
            [JsonPropertyName("op_user_id")]
            public string? OpUserId { get; set; }

            /// <summary>
            /// 是否清空座位
            /// </summary>
            [JsonPropertyName("clear_seat")]
            public bool? ClearSeat { get; set; }
        }

        /// <summary>
        /// 改变房间设置事件
        /// </summary>
        public class ChangeRoomSetting : IControlEvent
        {
            /// <summary>
            /// 操作用户 ID
            /// </summary>
            [JsonPropertyName("op_user_id")]
            public string? OpUserId { get; set; }

            /// <summary>
            /// 房间设置（必填）
            /// </summary>
            [JsonPropertyName("room_setting")]
            public string RoomSetting { get; set; } = string.Empty;
        }

        /// <summary>
        /// 改变用户身份事件
        /// </summary>
        public class ChangeUserIdentity : IControlEvent
        {
            /// <summary>
            /// 用户 ID（必填）
            /// </summary>
            [JsonPropertyName("user_id")]
            public string UserId { get; set; } = string.Empty;

            /// <summary>
            /// 身份（必填）
            /// </summary>
            [JsonPropertyName("identity")]
            public int Identity { get; set; }
        }

        /// <summary>
        /// 房间座位同步事件
        /// </summary>
        public class RoomSeatSync : IControlEvent
        {
        }

        /// <summary>
        /// 刷新用户信息事件
        /// </summary>
        public class RefreshUserInfo : IControlEvent
        {
            /// <summary>
            /// 用户 ID（必填）
            /// </summary>
            [JsonPropertyName("user_id")]
            public string UserId { get; set; } = string.Empty;
        }

        /// <summary>
        /// 快速开始游戏事件
        /// </summary>
        public class QuickStartGame : IControlEvent
        {
            /// <summary>
            /// 设置
            /// </summary>
            [JsonPropertyName("setting")]
            public string? Setting { get; set; }

            /// <summary>
            /// 是否开始游戏
            /// </summary>
            [JsonPropertyName("start_game")]
            public bool? StartGame { get; set; }

            /// <summary>
            /// 用户 ID 列表
            /// </summary>
            [JsonPropertyName("user_ids")]
            public List<string>? UserIds { get; set; }
        }

        /// <summary>
        /// 发放道具事件
        /// </summary>
        public class IssueProps : IControlEvent
        {
            /// <summary>
            /// 唯一 ID（必填）
            /// </summary>
            [JsonPropertyName("unique_id")]
            public string UniqueId { get; set; } = string.Empty;

            /// <summary>
            /// 用户 ID（必填）
            /// </summary>
            [JsonPropertyName("user_id")]
            public string UserId { get; set; } = string.Empty;

            /// <summary>
            /// 道具详情列表
            /// </summary>
            [JsonPropertyName("details")]
            public List<PropDetail>? Details { get; set; }

            /// <summary>
            /// 额外信息
            /// </summary>
            [JsonPropertyName("extra")]
            public string? Extra { get; set; }

            /// <summary>
            /// 道具详情
            /// </summary>
            public class PropDetail
            {
                /// <summary>
                /// 道具 ID（必填）
                /// </summary>
                [JsonPropertyName("prop_id")]
                public string PropId { get; set; } = string.Empty;

                /// <summary>
                /// 数量（必填）
                /// </summary>
                [JsonPropertyName("num")]
                public long Num { get; set; }

                /// <summary>
                /// 持续时间
                /// </summary>
                [JsonPropertyName("duration")]
                public long? Duration { get; set; }

                /// <summary>
                /// 是否重置持续时间
                /// </summary>
                [JsonPropertyName("duration_reset")]
                public bool? DurationReset { get; set; }
            }
        }

        /// <summary>
        /// 获取背包状态事件
        /// </summary>
        public class FetchBagStatus : IControlEvent
        {
            /// <summary>
            /// 用户 ID（必填）
            /// </summary>
            [JsonPropertyName("user_id")]
            public string UserId { get; set; } = string.Empty;
        }

        /// <summary>
        /// 查询发放道具状态事件
        /// </summary>
        public class QueryIssuePropStatus : IControlEvent
        {
            /// <summary>
            /// 唯一 ID（必填）
            /// </summary>
            [JsonPropertyName("unique_id")]
            public string UniqueId { get; set; } = string.Empty;
        }

        /// <summary>
        /// 装备道具事件
        /// </summary>
        public class EquippedProp : IControlEvent
        {
            /// <summary>
            /// 用户 ID（必填）
            /// </summary>
            [JsonPropertyName("user_id")]
            public string UserId { get; set; } = string.Empty;

            /// <summary>
            /// 装备道具 ID（必填）
            /// </summary>
            [JsonPropertyName("equipped_prop_id")]
            public string EquippedPropId { get; set; } = string.Empty;
        }

        /// <summary>
        /// 卸下道具事件
        /// </summary>
        public class UnequippedProp : IControlEvent
        {
            /// <summary>
            /// 用户 ID（必填）
            /// </summary>
            [JsonPropertyName("user_id")]
            public string UserId { get; set; } = string.Empty;

            /// <summary>
            /// 卸下道具 ID（必填）
            /// </summary>
            [JsonPropertyName("unequipped_prop_id")]
            public string UnequippedPropId { get; set; } = string.Empty;
        }
    }
}