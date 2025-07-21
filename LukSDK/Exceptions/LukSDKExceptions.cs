using System;

namespace LukSDK.Exceptions
{
    /// <summary>
    /// LukSDK 异常常量定义
    /// </summary>
    public static class LukSDKExceptions
    {
        /// <summary>
        /// 服务器内部异常
        /// </summary>
        public static readonly LukSDKException InternalError = new LukSDKException(100000, "LukSDK: 服务器内部异常");

        /// <summary>
        /// 参数错误
        /// </summary>
        public static readonly LukSDKException ParamError = new LukSDKException(100001, "LukSDK: 参数错误");

        /// <summary>
        /// 请稍后再试
        /// </summary>
        public static readonly LukSDKException RetryError = new LukSDKException(100002, "LukSDK: 请稍后再试");

        /// <summary>
        /// 资源不存在
        /// </summary>
        public static readonly LukSDKException ContentError = new LukSDKException(100003, "LukSDK: 资源不存在");

        /// <summary>
        /// 渠道已禁用
        /// </summary>
        public static readonly LukSDKException ChannelError = new LukSDKException(100004, "LukSDK: 渠道已禁用");

        /// <summary>
        /// 签名校验失败
        /// </summary>
        public static readonly LukSDKException SignError = new LukSDKException(100005, "LukSDK: 签名校验失败");

        /// <summary>
        /// 未登录或 Token 已过期
        /// </summary>
        public static readonly LukSDKException LoginError = new LukSDKException(100006, "LukSDK: 未登录或 Token 已过期");

        /// <summary>
        /// 渠道方回调地址响应解析失败
        /// </summary>
        public static readonly LukSDKException CallbackError = new LukSDKException(100007, "LukSDK: 渠道方回调地址响应解析失败");

        /// <summary>
        /// 解析错误码
        /// </summary>
        /// <param name="error">异常对象</param>
        /// <returns>错误代码</returns>
        public static int ParseErrorCode(Exception error)
        {
            return error is LukSDKException lukSDKException 
                ? lukSDKException.Code 
                : InternalError.Code;
        }

        /// <summary>
        /// 根据错误代码获取对应的异常
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <returns>对应的异常实例，如果找不到则返回 InternalError</returns>
        public static LukSDKException GetExceptionByCode(int code)
        {
            switch (code)
            {
                case 100000:
                    return InternalError;
                case 100001:
                    return ParamError;
                case 100002:
                    return RetryError;
                case 100003:
                    return ContentError;
                case 100004:
                    return ChannelError;
                case 100005:
                    return SignError;
                case 100006:
                    return LoginError;
                case 100007:
                    return CallbackError;
                default:
                    return InternalError;
            }
        }
    }
}