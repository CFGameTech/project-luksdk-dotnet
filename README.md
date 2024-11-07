# 介绍
本项目为 .Net 版本的 LukSDK，可直接引入使用，其中提供了需接入接口的通用实现，仅需结合业务逻辑将其返回即可。

> 仅需将 HTTP 请求转换为对应结构体后调用相关函数并填充返回值即可，关于参数的校验等行为交由 SDK 内部处理。

# 安装
.Net Core 4.8.1
```shell
dotnet add package luksdk
```

.Net Core Standard 2.0
```shell
dotnet add package luksdk-standard-2
```

# 示例代码
```csharp
using System;
using System.Text.Json;
using project_luksdk_dotnet;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
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
```