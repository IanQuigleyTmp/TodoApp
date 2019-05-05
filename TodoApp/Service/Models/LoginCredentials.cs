using Newtonsoft.Json;

namespace Service.Models
{
    public class LoginCredentials
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

}