using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LukSDK.Client;
using LukSDK.Config;
using LukSDK.Models.ApiModels;
using LukSDK.Utils;
using LukSDK.Exceptions;

namespace TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== LukSDK 测试控制台 ===");
            Console.WriteLine();

            try
            {
                // 运行初始化示例
                Console.WriteLine("1. 运行初始化示例...");
                RunInitializationExample();
                Console.WriteLine("✅ 初始化示例完成");
                Console.WriteLine();

                // 运行字段映射测试
                Console.WriteLine("2. 运行字段映射测试...");
                RunFieldMappingTest();
                Console.WriteLine("✅ 字段映射测试完成");
                Console.WriteLine();

                // 运行签名工具示例
                Console.WriteLine("3. 运行签名工具示例...");
                RunSignatureUtilsExample();
                Console.WriteLine("✅ 签名工具示例完成");
                Console.WriteLine();

                // 运行 API 调用示例（可选，需要真实的 API 凭据）
                Console.WriteLine("4. 运行 API 调用示例...");
                await RunApiCallExample();
                Console.WriteLine("✅ API 调用示例完成");
                Console.WriteLine();

                Console.WriteLine("=== 所有测试完成 ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 发生错误: {ex.Message}");
                Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
            }

            Console.WriteLine();
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }

        /// <summary>
        /// SDK 初始化示例
        /// </summary>
        static void RunInitializationExample()
        {
            // 方式 1: 使用构造函数
            var config1 = new LukSDKConfig(
                appId: 12345,
                appSecret: "your-app-secret",
                domain: "https://api.example.com"
            );

            // 方式 2: 使用构建器模式
            var config2 = LukSDKConfig.Builder()
                .SetAppId(12345)
                .SetAppSecret("your-app-secret")
                .SetDomain("https://api.example.com")
                .SetHttpClient(new HttpClient())
                .Build();

            // 创建 SDK 客户端
            using var client = new LukSDKClient(config1);

            Console.WriteLine("SDK 初始化成功");
        }

        /// <summary>
        /// 字段映射测试
        /// </summary>
        static void RunFieldMappingTest()
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true
            };

            // 测试 GetGameServiceListRequest (使用 c_id)
            var gameServiceRequest = new GetGameServiceListRequest
            {
                AppId = 12345,
                Timestamp = 1640995200,
                Sign = "test-signature"
            };

            var gameServiceJson = JsonSerializer.Serialize(gameServiceRequest, jsonOptions);
            Console.WriteLine("GetGameServiceListRequest JSON:");
            Console.WriteLine(gameServiceJson);
            Console.WriteLine("应该包含 'c_id': " + gameServiceJson.Contains("c_id"));
            Console.WriteLine();

            // 测试 PublishControlEventRequest (使用 app_id)
            var controlEventRequest = new PublishControlEventRequest
            {
                AppId = 12345,
                GameId = 1001,
                Type = 1,
                Timestamp = 1640995200,
                Sign = "test-signature"
            };

            var controlEventJson = JsonSerializer.Serialize(controlEventRequest, jsonOptions);
            Console.WriteLine("PublishControlEventRequest JSON:");
            Console.WriteLine(controlEventJson);
            Console.WriteLine("应该包含 'app_id': " + controlEventJson.Contains("app_id"));
            Console.WriteLine();
        }

        /// <summary>
        /// 签名工具使用示例
        /// </summary>
        static void RunSignatureUtilsExample()
        {
            var appSecret = "your-app-secret";
            
            // 创建请求对象
            var request = new GetGameServiceListRequest
            {
                AppId = 12345,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };

            // 生成签名
            var signature = SignatureUtils.GenerateSignature(appSecret, request);
            Console.WriteLine($"生成的签名: {signature}");

            // 设置签名到请求对象
            request.Sign = signature;

            // 验证签名
            var isValid = SignatureUtils.VerifySignature(appSecret, request, signature);
            Console.WriteLine($"签名验证结果: {isValid}");

            // 验证错误的签名
            var isInvalid = SignatureUtils.VerifySignature(appSecret, request, "wrong-signature");
            Console.WriteLine($"错误签名验证结果: {isInvalid}");
        }

        /// <summary>
        /// API 调用示例（需要真实的 API 凭据）
        /// </summary>
        static async Task RunApiCallExample()
        {
            try
            {
                var config = new LukSDKConfig(
                    appId: 0,
                    appSecret: "",
                    domain: "https://aaa.bbb.ccc"
                );

                using var sdk = new LukSDKClient(config);
                
                var gameServiceRequest = new GetGameServiceListRequest
                {
                    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                };

                Console.WriteLine("正在调用 GetGameServiceList API...");
                var response = await sdk.GetGameServiceListAsync(gameServiceRequest);
                
                Console.WriteLine($"响应代码: {response.Code}");
                Console.WriteLine($"响应消息: {response.Message}");
                
                if (response.IsSuccess && response.Data?.GameList != null)
                {
                    Console.WriteLine($"获取到 {response.Data.GameList.Count} 个游戏");
                    foreach (var game in response.Data.GameList)
                    {
                        Console.WriteLine($"  - {game.GameName} (ID: {game.GameId})");
                    }
                }
            }
            catch (LukSDKException ex)
            {
                Console.WriteLine($"SDK 异常: [{ex.Code}] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"其他异常: {ex.Message}");
            }
        }
    }
}