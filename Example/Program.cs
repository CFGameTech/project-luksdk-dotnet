using project_luksdk_dotnet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseUrls("http://localhost:8080");

var app = builder.Build();
var sdk = new SDK("fa7ad21fdbe10218024f88538a86");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var group = app.MapGroup("/sdk");
group.MapPost("/get_channel_token", (GetChannelTokenRequest request) =>
{
    var response = sdk.GetChannelToken(request, _ => (new GetChannelTokenResponse
    {
        Token = "my-token",
        LeftTime = 7200
    }, null));
    Console.WriteLine("get_channel_token - request: {0}, response: {1}", request, response);
    return response;
});

group.MapPost("/refresh_channel_token", (RefreshChannelTokenRequest request) =>
{
    var response = sdk.RefreshChannelToken(request, _ => (new RefreshChannelTokenResponse
    {
        Token = "my-token",
        LeftTime = 7200
    }, null));
    Console.WriteLine("refresh_channel_token - request: {0}, response: {1}", request, response);
    return response;
});

group.MapPost("/get_channel_user_info", (GetChannelUserInfoRequest request) =>
{
    var response = sdk.GetChannelUserInfo(request, req => (new GetChannelUserInfoResponse
    {
        UserId = req.UserId,
        Name = "my-name",
        Avatar = "",
        Coins = 100000
    }, null));
    Console.WriteLine("/get_channel_user_info - request: {0}, response: {1}", request, response);
    return response;
});

group.MapPost("/create_channel_order", (CreateChannelOrderRequest request) =>
{
    var response = sdk.CreateChannelOrder(request, req =>
    {
        List<CreateChannelOrderResponseEntry> entries = req.Data.Select(datum =>
            new CreateChannelOrderResponseEntry
            {
                UserId = datum.UserId,
                OrderId = datum.GameOrderId,
                Coins = 1000000,
                Status = 1,
            }).ToList();

        CreateChannelOrderResponse resp = new CreateChannelOrderResponse();
        resp.AddRange(entries);
        return (resp, null);
    });
    Console.WriteLine("create_channel_order - request: {0}, response: {1}", request, response);
    return response;
});

group.MapPost("/notify_channel_order", (NotifyChannelOrderRequest request) =>
{
    var response = sdk.NotifyChannelOrder(request, req =>
    {
        List<NotifyChannelOrderResponseEntry> entries = req.Data.Select(datum =>
            new NotifyChannelOrderResponseEntry
            {
                UserId = datum.UserId,
                OrderId = datum.GameOrderId,
                Coins = 1000000,
                Score = 100000
            }).ToList();
        NotifyChannelOrderResponse resp = new NotifyChannelOrderResponse();
        resp.AddRange(entries);
        return (resp, null);
    });
    Console.WriteLine("notify_channel_order - request: {0}, response: {1}", request, response);
    return response;
});

group.MapPost("/notify_game", (NotifyGameRequest request) =>
{
    var response = sdk.NotifyGame(request, _ => (new NotifyGameResponse(), null));
    Console.WriteLine("notify_game - request: {0}, response: {1}", request, response);
    return response;
});

app.Run();
