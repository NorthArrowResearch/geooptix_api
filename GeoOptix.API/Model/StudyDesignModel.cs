using Newtonsoft.Json;

namespace GeoOptix.API.Model
{
    public class StudyDesignModel
    {
        [JsonProperty("designid")]
        public int DesignID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("dateregistered")]
        public string DateRegistered { get; set; }

        [JsonProperty("startyear")]
        public string StartYear { get; set; }

        [JsonProperty("designApiEndpoint")]
        public string DesignApiEndpoint { get; set; }
    }
}