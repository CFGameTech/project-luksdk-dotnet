using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using LukSDK.Client;
using LukSDK.Config;
using LukSDK.Models.ApiModels;
using LukSDK.Models.Callbacks;
using LukSDK.Utils;
using E = LukSDK.Enums.LukSDKEnums.ControlEventType;

static void Check(bool condition, string name) { if (!condition) throw new Exception(name); }
var options = LukSDKJson.CreateOptions();
var parameters = new Dictionary<string, object?> { ["c_id"]=42, ["ratio"]=0.5, ["text"]="0", ["truth"]=true, ["false"]=false, ["zero"]=0, ["nested"]=new {x=1}, ["items"]=new[]{1}, ["sign"]="ignored", ["empty"]="", ["missing"]=null };
var expected = Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes("c_id=42&ratio=0.5&text=0&truth=true&key=unit-key")));
Check(SignatureUtils.GenerateSignature("unit-key",parameters)==expected,"scalar signature");
Check(SignatureUtils.GenerateSignature("unit-key",new {})==Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes("&key=unit-key"))),"empty signature");
using (var raw = JsonDocument.Parse("{\"number\":9007199254740993}"))
{
    var large = Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes("number=9007199254740993&key=unit-key")));
    Check(SignatureUtils.GenerateSignature("unit-key",raw.RootElement)==large,"int64 precision");
}
Console.WriteLine("PASS scalar signatures and int64 precision");

Check(E.JoinGame==1 && E.LeaveGame==2 && E.ChangeReadyStatus==3 && E.KickPlayer==4 && E.StartGame==5 && E.ForceCloseGame==6 && E.ChangeRoomSetting==7 && E.ChangeUserIdentity==8 && E.RoomSeatSync==9 && E.RefreshUserInfo==10 && E.QuickStartGame==11 && E.AddRobot==13 && E.UserItemGrant==1000 && E.FetchBagStatus==1001 && E.QueryIssuePropStatus==1002 && E.EquippedProp==1003 && E.UnequippedProp==1004,"protocol events");
Console.WriteLine("PASS all public control event constants");

var user = JsonSerializer.Deserialize<GetChannelUserInfoRequest>("{\"c_id\":42,\"g_id\":\"7\",\"c_uid\":\"u\",\"token\":\"opaque\",\"new_field\":true}",options)!;
Check(user.ChannelId==42 && user.GameId==7 && user.Token=="opaque","user callback");
var refresh = JsonSerializer.Deserialize<RefreshChannelTokenRequest>("{\"c_id\":42}",options)!;
Check(refresh.ChannelId==42,"refresh callback");
var userResponse = new GetChannelUserInfoResponse { Data = new GetChannelUserInfoResponse.UserData {UserId="u",Nickname="name"} };
using (var doc = JsonDocument.Parse(JsonSerializer.Serialize(userResponse, options)))
    Check(doc.RootElement.GetProperty("data").GetProperty("name").GetString()=="name" && doc.RootElement.GetProperty("data").GetProperty("c_uid").GetString()=="u","user response fields");
var create = JsonSerializer.Deserialize<CreateChannelOrderRequest>("{\"data\":[{\"c_id\":42,\"g_id\":7,\"c_room_id\":\"room\",\"score_cost\":2,\"item_id\":\"p\",\"num\":3}]}",options)!;
Check(create.Data![0].RoomId=="room" && create.Data[0].ScoreCost==2 && create.Data[0].Num==3,"create callback");
var notify = JsonSerializer.Deserialize<NotifyChannelOrderRequest>("{\"data\":[{\"status\":2,\"score_cost\":2,\"score_award\":3}]}",options)!;
Check(notify.Data![0].Status==2 && notify.Data[0].ScoreAward==3,"notify order");
Check(new NotifyChannelOrderResult{Score=4}.Score==4,"remaining score");
var end = JsonSerializer.Deserialize<NotifyEventGameOver>("{\"user_results\":{\"u\":{\"score\":4}}}",options)!;
Check(end.UserResults!["u"].Score==4,"result map");
Console.WriteLine("PASS callback field names and coverage");

var order = JsonSerializer.Deserialize<QueryOrderResponse>("{\"code\":\"0\",\"data\":{\"anchor_draw\":0.5,\"coins_official_draw\":0.25},\"new_field\":true}",options)!;
Check(order.Data!.AnchorDraw==0.5 && order.Data.CoinsOfficialDraw==0.25,"fractional commissions");
Check(JsonSerializer.Deserialize<QueryOrderResponse>("{\"code\":1,\"data\":null}",options)!.Data==null,"null data");
var props = JsonSerializer.Deserialize<ControlEventResponseModels.QueryIssuePropStatusResponse>("{\"props\":[{\"prop_id\":\"p\",\"num\":1}]}",options)!;
Check(props.Details!.Count==1,"props alias");
var bag = JsonSerializer.Deserialize<ControlEventResponseModels.FetchBagStatusResponse>("{\"props\":[{\"type\":2}]}",options)!;
Check(bag.PropList![0].PropType==2,"prop type");
var seat = JsonSerializer.Deserialize<ControlEventResponseModels.RoomSeatSyncResponse>("{\"players\":[{\"user_id\":\"u\",\"is_robot\":true}]}",options)!;
Check(seat.Players![0].IsRobot==true,"seat players");
var quick = new ControlEventModels.QuickStartGame {StartExt="ext",UserInfos=new(){new(){UserId="u",RobotLevel=2}}};
using (var doc = JsonDocument.Parse(JsonSerializer.Serialize(quick,options)))
    Check(doc.RootElement.GetProperty("user_infos")[0].GetProperty("robot_level").GetInt32()==2,"quick start");
Check(new ControlEventModels.AddRobot {UserId="bot",Level=2}.Level==2,"robot model");
Console.WriteLine("PASS native JSON options and control models");

var handler = new CaptureHandler();
using var http = new HttpClient(handler);
using var client = new LukSDKClient(new LukSDKConfig(42,"unit-key","https://example.com",http));
await client.GetGameServiceListAsync(new());
await client.QueryNotifyEventAsync(new(){GameId=7,PageNo=1,PageSize=1});
await client.QueryOrderAsync(new(){GameId=7,AppOrderNo="missing"});
await client.PublishControlEventAsync(new(){GameId=7,Type=9,Data="{\"sync_mode\":true}"});
Check(handler.Count==4,"all APIs called");
Console.WriteLine("PASS four HTTP requests sign final wire fields");

sealed class CaptureHandler : HttpMessageHandler
{
    public int Count {get;private set;}
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using var doc = JsonDocument.Parse(await request.Content!.ReadAsStringAsync(cancellationToken));
        var root = doc.RootElement;
        string key = request.RequestUri!.AbsolutePath.EndsWith("get_game_service_list") ? "c_id" : "app_id";
        if(root.GetProperty(key).GetInt64()!=42 || root.GetProperty("timestamp").GetInt64()<=0 || root.GetProperty("sign").GetString()!=SignatureUtils.GenerateSignature("unit-key",root)) throw new Exception("wire signature or fields");
        Count++;
        return new HttpResponseMessage(HttpStatusCode.OK){Content=new StringContent("{\"code\":0,\"data\":{},\"new_field\":true}",Encoding.UTF8,"application/json")};
    }
}
