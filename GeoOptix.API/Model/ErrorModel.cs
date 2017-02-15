using Newtonsoft.Json;

namespace GeoOptix.API.Model
{
    public class ErrorModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}