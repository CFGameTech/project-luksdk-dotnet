using System;
using System.Collections.Generic;

namespace project_luksdk_dotnet
{
    public static class Errors
    {
        private static readonly Dictionary<Exception, int> ErrorMap = new Dictionary<Exception, int>();

        public static readonly Exception ErrInvalidParams = RegError(1000, "invalid params");                   // 参数有误
        public static readonly Exception ErrInvalidChannel = RegError(1001, "invalid channel");                 // 渠道有误
        public static readonly Exception ErrInvalidChannelOrder = RegError(1002, "invalid channel request");    // 渠道请求异常
        public static readonly Exception ErrInvalidSignature = RegError(1003, "invalid signature");             // 签名有误
        public static readonly Exception ErrInvalidGame = RegError(1004, "invalid game");                       // 游戏有误
        public static readonly Exception ErrChannelDataException = RegError(1005, "channel data exception");    // 渠道返回数据异常
        public static readonly Exception ErrRepeatOrder = RegError(1006, "repeat order");                       // 重复下订单
        public static readonly Exception ErrOrderFailed = RegError(1007, "order failed");                       // 下单失败
        public static readonly Exception ErrOrderNotExist = RegError(1008, "order not exist");                  // 订单不存在

        private static Exception RegError(int code, string message)
        {
            var exception = new Exception(message);
            ErrorMap[exception] = code;
            return exception;
        }
        
        public static int GetErrorCode(Exception ex)
        {
            return ErrorMap[ex];
        }
    }
}