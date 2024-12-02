using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace project_luksdk_dotnet
{
    public class SDK
    {
        private readonly string _signSecret;
        private readonly string _domain;
        private readonly string apiPrefix = "/sdk";

        public SDK(string signSecret, string domain)
        {
            _signSecret = signSecret;
            _domain = domain;
        }
        
        /**
        * 获取包含游戏列表的响应
        */
        public Response<GetGameServiceListResponse> GetGameServiceList(int channelId)
        {
            var request = new GetGameServiceListRequest
            {
                ChannelId = channelId,
                Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
            };
            return GetGameServiceList(request);
        }
        
        /**
         * 获取包含游戏列表的响应，给定的请求中如果签名字段如果为空字符串将自动计算签名
         */
        public Response<GetGameServiceListResponse> GetGameServiceList(GetGameServiceListRequest request)
        {
            if (string.IsNullOrEmpty(_domain))
            {
                throw new Exception("domain is empty");
            }

            var url = $"{_domain}{apiPrefix}/get_game_service_list/";
            var jsonRequest = JsonSerializer.Serialize(request);

            if (string.IsNullOrEmpty(request.Sign))
            {
                request.Sign = GenerateSignature(request);
            }

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";

            var data = Encoding.UTF8.GetBytes(jsonRequest);
            httpRequest.ContentLength = data.Length;

            using (var stream = httpRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)httpRequest.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Url: {url} Error Code: {(int)response.StatusCode}");
            
            using var reader = new StreamReader(response.GetResponseStream()!);
            var responseText = reader.ReadToEnd();
            var responseObject = JsonSerializer.Deserialize<Response<GetGameServiceListResponse>>(responseText);

            if (responseObject.Code != 0)
            {
                throw new Exception($"Error Code: {responseObject.Code} Message: {responseObject.Msg}");
            }
            return responseObject;

        }
        
                /**
        * 发放道具
        */
        public Response<IssuancePropsResponse> IssuanceProps(int channelId, int gameId, List<IssuancePropsRequestEntry> data)
        {
            var request = new IssuancePropsRequest()
            {
                ChannelId = channelId,
                GameId = gameId,
                Data = data,
                Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
            };
            return IssuanceProps(request);
        }
        
        /**
         * 发放道具，给定的请求中如果签名字段如果为空字符串将自动计算签名
         */
        public Response<IssuancePropsResponse> IssuanceProps(IssuancePropsRequest request)
        {
            if (string.IsNullOrEmpty(_domain))
            {
                throw new Exception("domain is empty");
            }
            
            if (string.IsNullOrEmpty(request.Sign))
            {
                request.Sign = GenerateSignature(request);
            }

            var url = $"{_domain}{apiPrefix}/issuance_props/";
            var jsonRequest = JsonSerializer.Serialize(request);


            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";

            var data = Encoding.UTF8.GetBytes(jsonRequest);
            httpRequest.ContentLength = data.Length;

            using (var stream = httpRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)httpRequest.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Url: {url} Error Code: {(int)response.StatusCode}");
            using (var responseStream = response.GetResponseStream())
            {
                if (responseStream == null)
                {
                    throw new Exception("Response stream is null.");
                }
                
                using (var reader = new StreamReader(responseStream))
                {
                    var responseText = reader.ReadToEnd();
                    var responseObject = JsonSerializer.Deserialize<Response<IssuancePropsResponse>>(responseText);

                    if (responseObject.Code != 0)
                    {
                        throw new Exception($"Error Code: {responseObject.Code} Message: {responseObject.Msg}");
                    }
                    return responseObject;
                }
            }

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

    public class Test
    {
        public static void Main()
        {
            var sdk = new SDK("fa7ad21fdbe10218024f88538a86", "https://api.luk.live");
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