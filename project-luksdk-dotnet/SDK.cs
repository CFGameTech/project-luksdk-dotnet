using System;
using System.Collections.Generic;

namespace project_luksdk_dotnet
{
      public class SDK
    {
        private readonly string _signSecret;

        public SDK(string signSecret)
        {
            _signSecret = signSecret;
        }

        // 验证签名是否正确
        public void VerifySignature(string sign, object parameters)
        {
            var verify = SignatureHelper.Signature(_signSecret, parameters);
            if (verify != sign)
            {
                throw new InvalidOperationException("Invalid signature");
            }
        }

        // 生成签名
        public string GenerateSignature(object parameters)
        {
            return SignatureHelper.Signature(_signSecret, parameters);
        }

        // CFGame向接入方获取用户令牌
        public Response<GetChannelTokenResponse> GetChannelToken(GetChannelTokenRequest request, params RequestHandler<GetChannelTokenRequest, GetChannelTokenResponse>[] successHandlers)
        {
            return GenerateHandler(_signSecret, request.Sign, request, successHandlers);
        }

        // 刷新用户令牌过期时间
        public Response<RefreshChannelTokenResponse> RefreshChannelToken(RefreshChannelTokenRequest request, params RequestHandler<RefreshChannelTokenRequest, RefreshChannelTokenResponse>[] successHandlers)
        {
            return GenerateHandler(_signSecret, request.Sign, request, successHandlers);
        }

        // 获取渠道用户信息
        public Response<GetChannelUserInfoResponse> GetChannelUserInfo(GetChannelUserInfoRequest request, params RequestHandler<GetChannelUserInfoRequest, GetChannelUserInfoResponse>[] successHandlers)
        {
            return GenerateHandler(_signSecret, request.Sign, request, successHandlers);
        }

        // 向渠道下订单
        public Response<CreateChannelOrderResponse> CreateChannelOrder(CreateChannelOrderRequest request, params RequestHandler<CreateChannelOrderRequest, CreateChannelOrderResponse>[] successHandlers)
        {
            return GenerateHandler(_signSecret, request.Sign, request, successHandlers);
        }

        // 下注开奖通知结果
        public Response<NotifyChannelOrderResponse> NotifyChannelOrder(NotifyChannelOrderRequest request, params RequestHandler<NotifyChannelOrderRequest, NotifyChannelOrderResponse>[] successHandlers)
        {
            return GenerateHandler(_signSecret, request.Sign, request, successHandlers);
        }

        // 向渠道通知游戏状态
        public Response<NotifyGameResponse> NotifyGame(NotifyGameRequest request, params RequestHandler<NotifyGameRequest, NotifyGameResponse>[] successHandlers)
        {
            return GenerateHandler(_signSecret, request.Sign, request, successHandlers);
        }

        // 生成处理逻辑的辅助方法
        private static Response<TResponse> GenerateHandler<TRequest, TResponse>(string signSecret, string requestSign, TRequest request, params RequestHandler<TRequest, TResponse>[] successHandlers)
        {
            var verify = SignatureHelper.Signature(signSecret, request);
            var response = new Response<TResponse>();

            if (verify != requestSign)
            {
                return response.WithError(Errors.ErrInvalidSignature, requestSign, verify);
            }

            foreach (var handler in successHandlers)
            {
                var (data, error) =handler(request);
                if (error != null)
                {
                    return response.WithError(Errors.ErrChannelDataException, error.Message);
                }
                response.Data = data;
            }

            return response.WithData(response.Data);
        }
    }
}

namespace project_luksdk_dotnet.Test
{
    public class Test
    {
        public static void Main()
        {
            var sdk = new SDK("fa7ad21fdbe10218024f88538a86");
            var request = new NotifyGameRequest()
            {
                GameId = 1,
                ChannelId = 1010997,
                NotifyType = NotifyType.NotifyTypeStartBefore,
                Timestamp = 123456789,
                Sign = "576E32AAD3EAFDB8BDBEFBFC47A268B6",
            };
            

            try
            {
                
                Console.WriteLine("verify_signature - request: {0}, {1}", request.Sign, sdk.GenerateSignature(request));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}