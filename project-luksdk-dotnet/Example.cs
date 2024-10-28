using System;
using System.Text.Json;

namespace project_luksdk_dotnet
{
    public class Example
    {
        private static void example()
        {
            // 初始化 SDK
            SDK sdk = new SDK("123456");
            
            // 来自 SDK 请求的参数结构
            GetChannelTokenRequest request = new GetChannelTokenRequest();
            request.CId = 1000;
            request.CUid = "123456789";
            request.Timestamp = 167456789;
            request.Sign = sdk.GenerateSignature(request);

            // 处理请求
            Response<GetChannelTokenResponse> resp = sdk.GetChannelToken(request, r =>
            {
                GetChannelTokenResponse response = new GetChannelTokenResponse();
                response.Token = "token"; // 生成 Token
                response.LeftTime = 7200; // 设置 Token 过期时间

                return (response, null);
            });
            
            // 将 resp 作为 JSON 写入 HTTP 响应
            Console.WriteLine(JsonSerializer.Serialize(resp));
        }
    }
}