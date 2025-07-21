using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LukSDK.Models.ApiModels
{
    /// <summary>
    /// 获取游戏服务列表响应
    /// </summary>
    public class GetGameServiceListResponse : BaseResponse
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
            /// 游戏列表
            /// </summary>
            [JsonPropertyName("game_list")]
            public List<Game>? GameList { get; set; }
        }

        /// <summary>
        /// 游戏信息类
        /// </summary>
        public class Game
        {
            /// <summary>
            /// 游戏 ID
            /// </summary>
            [JsonPropertyName("g_id")]
            public int? GameId { get; set; }

            /// <summary>
            /// 游戏名称
            /// </summary>
            [JsonPropertyName("g_name")]
            public string? GameName { get; set; }

            /// <summary>
            /// 游戏图标
            /// </summary>
            [JsonPropertyName("g_icon")]
            public string? GameIcon { get; set; }

            /// <summary>
            /// 游戏 URL
            /// </summary>
            [JsonPropertyName("g_url")]
            public string? GameUrl { get; set; }

            /// <summary>
            /// 返回游戏信息的字符串表示
            /// </summary>
            /// <returns>格式化的游戏信息</returns>
            public override string ToString()
            {
                return $"Game[Id={GameId}, Name={GameName}, Icon={GameIcon}, Url={GameUrl}]";
            }
        }
    }
}