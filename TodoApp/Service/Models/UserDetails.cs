using Newtonsoft.Json;

namespace Service.Models
{

    public class UserDetails : CommonResponse
    {
        [JsonIgnore]
        internal long UserId { get; set; }

        [JsonProperty("is_authenticated")]
        public bool IsAuthenticated { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        
        public static UserDetails Anonymous {  get { return new UserDetails() { DisplayName = "Guest", IsAuthenticated = false, AuthToken = "" }; } }
    }

}