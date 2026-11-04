using System.Text.Json;
using System.Text.Json.Serialization;

namespace LukSDK.Utils
{
    public static class LukSDKJson
    {
        public static JsonSerializerOptions CreateOptions() => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
    }
}
