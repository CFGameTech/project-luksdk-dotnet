using System;
using System.Net.Http;
using LukSDK.Exceptions;

namespace LukSDK.Config
{
    /// <summary>
    /// LukSDK 配置类
    /// </summary>
    public class LukSDKConfig
    {
        /// <summary>
        /// 应用 ID
        /// </summary>
        public long AppId { get; set; }

        /// <summary>
        /// 应用密钥
        /// </summary>
        public string AppSecret { get; set; } = string.Empty;

        /// <summary>
        /// API 域名
        /// </summary>
        public string Domain { get; set; } = string.Empty;

        /// <summary>
        /// HTTP 客户端
        /// </summary>
        public HttpClient HttpClient { get; set; } = new HttpClient();

        /// <summary>
        /// 初始化 LukSDKConfig 实例
        /// </summary>
        public LukSDKConfig()
        {
        }

        /// <summary>
        /// 初始化 LukSDKConfig 实例
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appSecret">应用密钥</param>
        /// <param name="domain">API 域名</param>
        public LukSDKConfig(long appId, string appSecret, string domain)
        {
            AppId = appId;
            AppSecret = appSecret ?? throw new ArgumentNullException(nameof(appSecret));
            Domain = domain ?? throw new ArgumentNullException(nameof(domain));
        }

        /// <summary>
        /// 初始化 LukSDKConfig 实例
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <param name="appSecret">应用密钥</param>
        /// <param name="domain">API 域名</param>
        /// <param name="httpClient">HTTP 客户端</param>
        public LukSDKConfig(long appId, string appSecret, string domain, HttpClient httpClient)
        {
            AppId = appId;
            AppSecret = appSecret ?? throw new ArgumentNullException(nameof(appSecret));
            Domain = domain ?? throw new ArgumentNullException(nameof(domain));
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// 验证配置有效性
        /// </summary>
        /// <exception cref="LukSDKException">配置无效时抛出异常</exception>
        public void Validate()
        {
            if (AppId <= 0)
            {
                throw LukSDKExceptions.ParamError.With("AppId must be greater than 0");
            }

            if (string.IsNullOrWhiteSpace(AppSecret))
            {
                throw LukSDKExceptions.ParamError.With("AppSecret cannot be null or empty");
            }

            if (string.IsNullOrWhiteSpace(Domain))
            {
                throw LukSDKExceptions.ParamError.With("Domain cannot be null or empty");
            }

            if (HttpClient == null)
            {
                throw LukSDKExceptions.ParamError.With("HttpClient cannot be null");
            }

            // 验证域名格式
            if (!Uri.TryCreate(Domain, UriKind.Absolute, out var uri) || 
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                throw LukSDKExceptions.ParamError.With("Domain must be a valid HTTP or HTTPS URL");
            }
        }

        /// <summary>
        /// 创建配置构建器
        /// </summary>
        /// <returns>配置构建器实例</returns>
        public static LukSDKConfigBuilder Builder()
        {
            return new LukSDKConfigBuilder();
        }

        /// <summary>
        /// 返回配置的字符串表示
        /// </summary>
        /// <returns>格式化的配置信息</returns>
        public override string ToString()
        {
            return $"LukSDKConfig[AppId={AppId}, Domain={Domain}, AppSecret=***]";
        }
    }

    /// <summary>
    /// LukSDK 配置构建器
    /// </summary>
    public class LukSDKConfigBuilder
    {
        private long _appId;
        private string _appSecret = string.Empty;
        private string _domain = string.Empty;
        private HttpClient? _httpClient;

        /// <summary>
        /// 设置应用 ID
        /// </summary>
        /// <param name="appId">应用 ID</param>
        /// <returns>构建器实例</returns>
        public LukSDKConfigBuilder SetAppId(long appId)
        {
            _appId = appId;
            return this;
        }

        /// <summary>
        /// 设置应用密钥
        /// </summary>
        /// <param name="appSecret">应用密钥</param>
        /// <returns>构建器实例</returns>
        public LukSDKConfigBuilder SetAppSecret(string appSecret)
        {
            _appSecret = appSecret ?? throw new ArgumentNullException(nameof(appSecret));
            return this;
        }

        /// <summary>
        /// 设置 API 域名
        /// </summary>
        /// <param name="domain">API 域名</param>
        /// <returns>构建器实例</returns>
        public LukSDKConfigBuilder SetDomain(string domain)
        {
            _domain = domain ?? throw new ArgumentNullException(nameof(domain));
            return this;
        }

        /// <summary>
        /// 设置 HTTP 客户端
        /// </summary>
        /// <param name="httpClient">HTTP 客户端</param>
        /// <returns>构建器实例</returns>
        public LukSDKConfigBuilder SetHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            return this;
        }

        /// <summary>
        /// 构建配置实例
        /// </summary>
        /// <returns>配置实例</returns>
        public LukSDKConfig Build()
        {
            var config = new LukSDKConfig
            {
                AppId = _appId,
                AppSecret = _appSecret,
                Domain = _domain,
                HttpClient = _httpClient ?? new HttpClient()
            };

            config.Validate();
            return config;
        }
    }
}