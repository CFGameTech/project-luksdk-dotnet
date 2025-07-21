using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LukSDK.Models.ApiModels
{
    /// <summary>
    /// 查询通知事件响应
    /// </summary>
    public class QueryNotifyEventResponse : BaseResponse
    {
        /// <summary>
        /// 响应数据
        /// </summary>
        [JsonPropertyName("data")]
        public ResponseData? Data { get; set; }

        /// <summary>
        /// 响应数据类
        /// </summary>
        public class ResponseData
        {
            /// <summary>
            /// 事件列表
            /// </summary>
            [JsonPropertyName("list")]
            public List<Event>? List { get; set; }
        }

        /// <summary>
        /// 事件信息类
        /// </summary>
        public class Event
        {
            /// <summary>
            /// 事件类型
            /// </summary>
            [JsonPropertyName("type")]
            public int? Type { get; set; }

            /// <summary>
            /// 事件数据（JSON 字符串）
            /// </summary>
            [JsonPropertyName("data")]
            public string? Data { get; set; }

            /// <summary>
            /// 返回事件信息的字符串表示
            /// </summary>
            /// <returns>格式化的事件信息</returns>
            public override string ToString()
            {
                return $"Event[Type={Type}, Data={Data}]";
            }
        }
    }
}