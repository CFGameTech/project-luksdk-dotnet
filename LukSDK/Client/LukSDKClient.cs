using System;
using System.Threading;
using System.Threading.Tasks;
using LukSDK.Config;
using LukSDK.Models.ApiModels;
using LukSDK.Utils;

namespace LukSDK.Client
{
    /// <summary>
    /// LukSDK 主客户端
    /// </summary>
    public class LukSDKClient : IDisposable
    {
        private readonly LukSDKConfig _config;
        private readonly ApiClient _apiClient;
        private bool _disposed = false;

        /// <summary>
        /// 获取 SDK 配置
        /// </summary>
        public LukSDKConfig Config => _config;

        /// <summary>
        /// 初始化 LukSDKClient 实例
        /// </summary>
        /// <param name="config">SDK 配置</param>
        public LukSDKClient(LukSDKConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _config.Validate();
            _apiClient = new ApiClient(_config);
        }

        /// <summary>
        /// 获取游戏服务列表
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>游戏服务列表响应</returns>
        public async Task<GetGameServiceListResponse> GetGameServiceListAsync(
            GetGameServiceListRequest request,
            CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.AppId == null || request.AppId == 0) request.AppId = _config.AppId;
            
            // 自动填充参数
            FillCommonRequestFields(request);

            return await _apiClient.PostAsync<GetGameServiceListRequest, GetGameServiceListResponse>(
                "/sdk/get_game_service_list", request, cancellationToken);
        }

        /// <summary>
        /// 查询通知事件
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>通知事件响应</returns>
        public async Task<QueryNotifyEventResponse> QueryNotifyEventAsync(
            QueryNotifyEventRequest request,
            CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.AppId == null || request.AppId == 0) request.AppId = _config.AppId;
            // 自动填充参数
            FillCommonRequestFields(request);

            return await _apiClient.PostAsync<QueryNotifyEventRequest, QueryNotifyEventResponse>(
                "/sdk/query_notify_event", request, cancellationToken);
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>订单查询响应</returns>
        public async Task<QueryOrderResponse> QueryOrderAsync(
            QueryOrderRequest request,
            CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.AppId == null || request.AppId == 0) request.AppId = _config.AppId;
            // 自动填充参数
            FillCommonRequestFields(request);

            return await _apiClient.PostAsync<QueryOrderRequest, QueryOrderResponse>(
                "/sdk/query_order", request, cancellationToken);
        }

        /// <summary>
        /// 发布控制事件
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>控制事件响应</returns>
        public async Task<PublishControlEventResponse> PublishControlEventAsync(
            PublishControlEventRequest request,
            CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.AppId == null || request.AppId == 0) request.AppId = _config.AppId;
            // 自动填充参数
            FillCommonRequestFields(request);

            return await _apiClient.PostAsync<PublishControlEventRequest, PublishControlEventResponse>(
                "/sdk/publish_control_event", request, cancellationToken);
        }

        /// <summary>
        /// 自动填充请求的公共字段
        /// </summary>
        /// <param name="request">请求对象</param>
        private void FillCommonRequestFields(Models.BaseRequest request)
        {
            // 如果 AppId 为空或为 0，使用配置的 AppId
            if (request.AppId == null || request.AppId == 0)
            {
                request.AppId = _config.AppId;
            }

            // 如果时间戳为空，使用当前时间戳
            if (request.Timestamp == null)
            {
                request.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }

            // 如果签名为空，自动生成签名
            if (string.IsNullOrEmpty(request.Sign))
            {
                request.Sign = SignatureUtils.GenerateSignature(_config.AppSecret, request);
            }
        }

        /// <summary>
        /// 检查对象是否已释放
        /// </summary>
        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(LukSDKClient));
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">是否正在释放托管资源</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                // 注意：我们不释放 HttpClient，因为它可能被外部管理
                // 用户应该自己管理 HttpClient 的生命周期
                _disposed = true;
            }
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~LukSDKClient()
        {
            Dispose(false);
        }
    }
}