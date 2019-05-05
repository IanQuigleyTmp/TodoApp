using Newtonsoft.Json;

namespace Service.Models
{
    public class CommonResponse
    {
        [JsonProperty("auth_token")]
        public string AuthToken { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }

}