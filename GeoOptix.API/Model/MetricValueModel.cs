using System;
using Newtonsoft.Json;

namespace GeoOptix.API.Model
{
    public class MetricValueModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }


        [Obsolete] public MetricValueModel() { }
    }
}