using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LukSDK.Models.ApiModels
{
    /// <summary>
    /// 发布控制事件响应
    /// </summary>
    public class PublishControlEventResponse : BaseResponse
    {
        /// <summary>
        /// 响应数据（动态类型，根据事件类型不同而不同）
        /// </summary>
        [JsonPropertyName("data")]
        public object? Data { get; set; }
    }

    /// <summary>
    /// 控制事件响应数据模型
    /// </summary>
    public static class ControlEventResponseModels
    {
        /// <summary>
        /// 控制事件响应接口
        /// </summary>
        public interface IControlEventResponse
        {
        }

        /// <summary>
        /// 获取背包状态响应
        /// </summary>
        public class FetchBagStatusResponse : IControlEventResponse
        {
            /// <summary>
            /// 道具列表
            /// </summary>
            [JsonPropertyName("props")]
            public List<Prop>? PropList { get; set; }

            /// <summary>
            /// 道具信息
            /// </summary>
            public class Prop
            {
                /// <summary>
                /// 过期时间
                /// </summary>
                [JsonPropertyName("expire_time")]
                public long? ExpireTime { get; set; }

                /// <summary>
                /// 是否已装备
                /// </summary>
                [JsonPropertyName("is_equipped")]
                public bool? IsEquipped { get; set; }

                /// <summary>
                /// 数量
                /// </summary>
                [JsonPropertyName("num")]
                public long? Num { get; set; }

                /// <summary>
                /// 道具 ID
                /// </summary>
                [JsonPropertyName("prop_id")]
                public string? PropId { get; set; }

                /// <summary>
                /// 道具类型
                /// </summary>
                [JsonPropertyName("prop_type")]
                public int? PropType { get; set; }

                /// <summary>
                /// 返回道具信息的字符串表示
                /// </summary>
                /// <returns>格式化的道具信息</returns>
                public override string ToString()
                {
                    return $"Prop[Id={PropId}, Type={PropType}, Num={Num}, IsEquipped={IsEquipped}]";
                }
            }
        }

        /// <summary>
        /// 查询发放道具状态响应
        /// </summary>
        public class QueryIssuePropStatusResponse : IControlEventResponse
        {
            /// <summary>
            /// 应用 ID
            /// </summary>
            [JsonPropertyName("app_id")]
            public long? AppId { get; set; }

            /// <summary>
            /// 唯一 ID
            /// </summary>
            [JsonPropertyName("unique_id")]
            public string? UniqueId { get; set; }

            /// <summary>
            /// 游戏 ID
            /// </summary>
            [JsonPropertyName("game_id")]
            public long? GameId { get; set; }

            /// <summary>
            /// 用户 ID
            /// </summary>
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }

            /// <summary>
            /// 创建时间
            /// </summary>
            [JsonPropertyName("created_time")]
            public long? CreatedTime { get; set; }

            /// <summary>
            /// 状态
            /// </summary>
            [JsonPropertyName("status")]
            public long? Status { get; set; }

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
                /// 道具 ID
                /// </summary>
                [JsonPropertyName("prop_id")]
                public string? PropId { get; set; }

                /// <summary>
                /// 数量
                /// </summary>
                [JsonPropertyName("num")]
                public long? Num { get; set; }

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

                /// <summary>
                /// 返回道具详情的字符串表示
                /// </summary>
                /// <returns>格式化的道具详情</returns>
                public override string ToString()
                {
                    return $"PropDetail[Id={PropId}, Num={Num}, Duration={Duration}]";
                }
            }
        }
    }
}