using Newtonsoft.Json;

namespace GeoOptix.API.Model
{
    public class MetricAttributeModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }


        public MetricAttributeModel(string name, string type)
        {
            Name = name;
            Type = type;
        }

    }
}