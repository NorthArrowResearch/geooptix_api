using System;
using Newtonsoft.Json;

namespace GeoOptix.API.Model
{
    public class MetricInstanceSummaryModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }


        [Obsolete]
        public MetricInstanceSummaryModel() {  }


        public MetricInstanceSummaryModel(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}