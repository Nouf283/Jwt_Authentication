using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Cookie_Based_Authentication.Authorization
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }
    }
}
