using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace project_luksdk_dotnet
{
    public enum NotifyType
    {
        NotifyTypeStartBefore = 1, // 游戏开始前状态
        NotifyTypeGaming,          // 游戏开始中状态
        NotifyTypeEnd              // 游戏结束状态
    }

    public enum Action
    {
        ActionJoinGame = 1,        // 加入游戏操作
        ActionExitGame,            // 退出游戏操作
        ActionSettingGame,         // 设置游戏操作
        ActionKickOut,             // 踢人操作
        ActionStartGame,           // 开始游戏操作
        ActionPrepare,             // 准备操作
        ActionCancelPrepare,       // 取消准备操作
        ActionGameEnd              // 游戏结束操作
    }

    public class GetChannelTokenRequest
    {
        [JsonPropertyName("c_id")]
        public int ChannelId { get; set; }
    
        [JsonPropertyName("c_uid")]
        public string UserId { get; set; }
    
        [JsonPropertyName("code")]
        public string Code { get; set; }
    
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    
        [JsonPropertyName("sign")]
        public string Sign { get; set; }

    }

    public class GetChannelTokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    
        [JsonPropertyName("left_time")]
        public long LeftTime { get; set; }
    }

    public class RefreshChannelTokenRequest
    {
        [JsonPropertyName("c_id")]
        public int ChannelId { get; set; }
        
        [JsonPropertyName("c_uid")]
        public string UserId { get; set; }
        
        [JsonPropertyName("token")]
        public string Token { get; set; }
        
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
        
        [JsonPropertyName("sign")]
        public string Sign { get; set; }
        
        [JsonPropertyName("left_time")]
        public long LeftTime { get; set; }
    }

    public class RefreshChannelTokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    
        [JsonPropertyName("left_time")]
        public long LeftTime { get; set; }
    }

    public class GetChannelUserInfoRequest
    {
        [JsonPropertyName("g_id")]
        public int GameId { get; set; }
        
        [JsonPropertyName("c_id")]
        public int ChannelId { get; set; }
        
        [JsonPropertyName("c_uid")]
        public string UserId { get; set; }
        
        [JsonPropertyName("token")]
        public string Token { get; set; }
        
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
        
        [JsonPropertyName("sign")]
        public string Sign { get; set; }
    }
    
    public class GetChannelUserInfoResponse 
    {
        [JsonPropertyName("c_uid")]
        public string UserId { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }
        
        [JsonPropertyName("coins")]
        public long Coins { get; set; }
    }

    public class CreateChannelOrderRequest
    {
        [JsonPropertyName("sign")]
        public string Sign { get; set; }
        
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
        
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
        
        [JsonPropertyName("data")]
        public List<CreateChannelOrderRequestEntry> Data { get; set; }
    }

    public class GetGameServiceListRequest
    {
        [JsonPropertyName("c_id")]
        public int ChannelId { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
        
        [JsonPropertyName("sign")]
        public string Sign { get; set; }
    }
    
    public class GetGameServiceListResponseEntry
    {
        [JsonPropertyName("g_id")]
        public int GameId { get; set; }
        
        [JsonPropertyName("g_name")]
        public string GameName { get; set; }
        
        [JsonPropertyName("g_icon")]
        public string GameIcon { get; set; }
        
        [JsonPropertyName("g_url")]
        public string GameUrl { get; set; }
    }
    
    public class GetGameServiceListResponse
    {
        [JsonPropertyName("game_list")]
        public List<GetGameServiceListResponseEntry> GameList { get; set; }
    }
    
    public class CreateChannelOrderRequestEntry 
    {
        [JsonPropertyName("c_id")]
        public int ChannelId { get; set; }
        
        [JsonPropertyName("c_uid")]
        public string UserId { get; set; }
        
        [JsonPropertyName("c_room_id")]
        public string RoomId { get; set; }
        
        [JsonPropertyName("g_id")]
        public int GameId { get; set; }
        
        [JsonPropertyName("coins_cost")]
        public long CoinsCost { get; set; }
        
        [JsonPropertyName("score_cost")]
        public long ScoreCost { get; set; }
        
        [JsonPropertyName("game_order_id")]
        public string GameOrderId { get; set; }
        
        [JsonPropertyName("token")]
        public string Token { get; set; }
        
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }

    public class CreateChannelOrderResponse : List<CreateChannelOrderResponseEntry>
    {
        
    }

    public class CreateChannelOrderResponseEntry
    {
        [JsonPropertyName("c_uid")]
        public string UserId { get; set; }
        
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }
        
        [JsonPropertyName("coins_cost")]
        public long Coins { get; set; }
        
        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
    
    public class NotifyChannelOrderRequest
    {
        [JsonPropertyName("sign")]
        public string Sign { get; set; }
    
        [JsonPropertyName("data")]
        public List<NotifyChannelOrderRequestEntry> Data { get; set; }
        
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
        
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
    }
    
    public class NotifyChannelOrderRequestEntry
    {
        [JsonPropertyName("c_id")]
        public int ChannelId { get; set; }
        
        [JsonPropertyName("c_uid")]
        public string UserId { get; set; }
        
        [JsonPropertyName("g_id")]
        public int GameId { get; set; }
        
        [JsonPropertyName("game_order_id")]
        public string GameOrderId { get; set; }
        
        [JsonPropertyName("token")]
        public string Token { get; set; }
        
        [JsonPropertyName("coins_cost")]
        public long CoinsCost { get; set; }
        
        [JsonPropertyName("coins_award")]
        public long CoinsAward { get; set; }
        
        [JsonPropertyName("score_cost")]
        public long ScoreCost { get; set; }
        
        [JsonPropertyName("score_award")]
        public long ScoreAward { get; set; }
        
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }

    public class NotifyChannelOrderResponse : List<NotifyChannelOrderResponseEntry>
    {
        
    }
    
    public class NotifyChannelOrderResponseEntry
    {
        [JsonPropertyName("c_uid")]
        public string UserId { get; set; }
    
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }
    
        [JsonPropertyName("coins")]
        public long Coins { get; set; }
    
        [JsonPropertyName("score")]
        public int Score { get; set; }
    }
    
    public class NotifyGameRequest
    {
        [JsonPropertyName("c_id")]
        public int ChannelId { get; set; }
    
        [JsonPropertyName("g_id")]
        public int GameId { get; set; }
    
        [JsonPropertyName("notify_type")]
        public NotifyType NotifyType { get; set; }
    
        [JsonPropertyName("ext")]
        public string Ext { get; set; }
    
        [JsonPropertyName("data")]
        public string Data { get; set; }
    
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    
        [JsonPropertyName("sign")]
        public string Sign { get; set; }
    
        public NotifyGameRequestStartBefore GetStartBefore()
        {
            return JsonSerializer.Deserialize<NotifyGameRequestStartBefore>(Data);
        }
    
        public NotifyGameRequestGaming GetGaming()
        {
            return JsonSerializer.Deserialize<NotifyGameRequestGaming>(Data);
        }
    
        public NotifyGameRequestEnd GetEnd()
        {
            return JsonSerializer.Deserialize<NotifyGameRequestEnd>(Data);
        }
    }
    

    public class NotifyGameRequestStartBefore
    {
        [JsonPropertyName("room_id")]
        public string RoomId { get; set; }
    
        [JsonPropertyName("round_id")]
        public string RoundId { get; set; }
    
        [JsonPropertyName("player_ready_status")]
        public Dictionary<string, bool> PlayerReadyStatus { get; set; }
    
        [JsonPropertyName("notify_action")]
        public Action NotifyAction { get; set; }
    
        [JsonPropertyName("game_setting")]
        public string GameSetting { get; set; }
    }

    public class NotifyGameRequestGaming
    {
        [JsonPropertyName("room_id")]
        public string RoomId { get; set; }
    
        [JsonPropertyName("round_id")]
        public string RoundId { get; set; }
    
        [JsonPropertyName("player_num")]
        public int PlayerNum { get; set; }
    
        [JsonPropertyName("player_uids")]
        public List<string> PlayerUids { get; set; }
    
        [JsonPropertyName("notify_action")]
        public Action NotifyAction { get; set; }
    }

    public class NotifyGameRequestEnd
    {
        [JsonPropertyName("room_id")]
        public string RoomId { get; set; }
    
        [JsonPropertyName("round_id")]
        public string RoundId { get; set; }
    
        [JsonPropertyName("rank")]
        public List<string> Rank { get; set; }
    
        [JsonPropertyName("is_force_end")]
        public bool IsForceEnd { get; set; }
    
        [JsonPropertyName("notify_action")]
        public Action NotifyAction { get; set; }
    }

    public class NotifyGameResponse
    {
        
    }

    // 通用响应类
    public class Response<T>
    {
        [JsonPropertyName("code")] 
        public int Code { get; set; }
    
        [JsonPropertyName("msg")] 
        public string Msg { get; set; }
    
        [JsonPropertyName("data")]
        public T Data { get; set; }

        public Response<T> WithError(Exception ex, params string[] messages)
        {
            if (ex == null) return this;

            Code = -1;
            Msg = messages.Length > 0 ? string.Join(", ", new[] { ex.Message }.Concat(messages)) : Errors.ErrInvalidParams.Message;
            return this;
        }

        public Response<T> WithData(T data)
        {
            Data = data;
            Msg = "成功";
            return this;
        }

        public bool IsSuccessful() => Code == 0;
    }
    
    public delegate (TResponse Data, Exception Error) RequestHandler<in TRequest, TResponse>(TRequest request);
}   
