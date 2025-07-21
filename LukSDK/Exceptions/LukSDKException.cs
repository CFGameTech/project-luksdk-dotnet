using System;

namespace LukSDK.Exceptions
{
    /// <summary>
    /// LukSDK 自定义异常类
    /// </summary>
    public class LukSDKException : Exception
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int Code { get; }

        /// <summary>
        /// 初始化 LukSDKException 实例
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="message">错误消息</param>
        public LukSDKException(int code, string message) : base(message)
        {
            Code = code;
        }

        /// <summary>
        /// 初始化 LukSDKException 实例
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="message">错误消息</param>
        /// <param name="innerException">内部异常</param>
        public LukSDKException(int code, string message, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

        /// <summary>
        /// 创建带有附加信息的异常实例
        /// </summary>
        /// <param name="additionalMessage">附加错误信息</param>
        /// <returns>新的异常实例</returns>
        public LukSDKException With(string additionalMessage)
        {
            return new LukSDKException(Code, $"{Message}: {additionalMessage}");
        }

        /// <summary>
        /// 创建带有内部异常的异常实例
        /// </summary>
        /// <param name="innerException">内部异常</param>
        /// <returns>新的异常实例</returns>
        public LukSDKException With(Exception innerException)
        {
            return new LukSDKException(Code, Message, innerException);
        }

        /// <summary>
        /// 创建带有附加信息和内部异常的异常实例
        /// </summary>
        /// <param name="additionalMessage">附加错误信息</param>
        /// <param name="innerException">内部异常</param>
        /// <returns>新的异常实例</returns>
        public LukSDKException With(string additionalMessage, Exception innerException)
        {
            return new LukSDKException(Code, $"{Message}: {additionalMessage}", innerException);
        }

        /// <summary>
        /// 返回异常的字符串表示
        /// </summary>
        /// <returns>包含错误代码和消息的字符串</returns>
        public override string ToString()
        {
            return $"LukSDKException [Code: {Code}]: {Message}";
        }
    }
}