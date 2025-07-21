using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LukSDK.Config;
using LukSDK.Exceptions;

namespace LukSDK.Client
{
    /// <summary>
    /// HTTP API 客户端
    /// </summary>
    internal class ApiClient
    {
        private readonly LukSDKConfig _config;
        private readonly JsonSerializerOptions _jsonOptions;

        /// <summary>
        /// 初始化 ApiClient 实例
        /// </summary>
        /// <param name="config">SDK 配置</param>
        public ApiClient(LukSDKConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _jsonOptions = CreateJsonSerializerOptions();
        }

        /// <summary>
        /// 发送 POST 请求
        /// </summary>
        /// <typeparam name="TRequest">请求类型</typeparam>
        /// <typeparam name="TResponse">响应类型</typeparam>
        /// <param name="endpoint">API 端点</param>
        /// <param name="request">请求对象</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>响应对象</returns>
        /// <exception cref="LukSDKException">请求失败时抛出异常</exception>
        public async Task<TResponse> PostAsync<TRequest, TResponse>(
            string endpoint,
            TRequest request,
            CancellationToken cancellationToken = default)
            where TRequest : class
            where TResponse : class
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentException("Endpoint cannot be null or empty", nameof(endpoint));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                // 序列化请求体
                var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // 构建完整的 URL
                var url = BuildUrl(endpoint);

                // 创建 HTTP 请求
                using var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
                httpRequest.Content = content;

                // 添加请求头
                httpRequest.Headers.Add("User-Agent", "dotnet/v1.0.0");

                // 发送请求
                using var response = await _config.HttpClient.SendAsync(httpRequest, cancellationToken);

                // 检查响应状态
                if (!response.IsSuccessStatusCode)
                {
                    throw LukSDKExceptions.InternalError.With($"HTTP error: {response.StatusCode} - {response.ReasonPhrase}");
                }

                // 读取响应内容
                var responseContent = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseContent))
                {
                    throw LukSDKExceptions.InternalError.With("Empty response body");
                }

                // 反序列化响应
                var result = JsonSerializer.Deserialize<TResponse>(responseContent, _jsonOptions);
                if (result == null)
                {
                    throw LukSDKExceptions.InternalError.With("Failed to deserialize response");
                }

                return result;
            }
            catch (OperationCanceledException)
            {
                throw LukSDKExceptions.RetryError.With("Request was cancelled");
            }
            catch (HttpRequestException ex)
            {
                throw LukSDKExceptions.InternalError.With("Network error occurred", ex);
            }
            catch (JsonException ex)
            {
                throw LukSDKExceptions.ParamError.With("JSON serialization/deserialization error", ex);
            }
            catch (LukSDKException)
            {
                // 重新抛出 LukSDK 异常
                throw;
            }
            catch (Exception ex)
            {
                throw LukSDKExceptions.InternalError.With($"Unexpected error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 构建完整的 API URL
        /// </summary>
        /// <param name="endpoint">API 端点</param>
        /// <returns>完整的 URL</returns>
        private string BuildUrl(string endpoint)
        {
            var domain = _config.Domain.TrimEnd('/');
            var path = endpoint.StartsWith("/") ? endpoint : "/" + endpoint;
            return domain + path;
        }

        /// <summary>
        /// 创建 JSON 序列化选项
        /// </summary>
        /// <returns>配置好的 JsonSerializerOptions</returns>
        private static JsonSerializerOptions CreateJsonSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                WriteIndented = false,
                IgnoreNullValues = true,
                // 支持未知字段，避免服务端添加新字段时报错
                IgnoreReadOnlyProperties = false
            };
        }
    }
}