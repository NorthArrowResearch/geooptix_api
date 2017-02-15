using System.Collections.Generic;
using Newtonsoft.Json;

namespace GeoOptix.API.Model
{
    public class MetricInstanceModel : MetricInstanceSummaryModel
    {
        [JsonProperty("metricSchemaUrl")]
        public string MetricSchemaUrl { get; set; }
        
        [JsonProperty("itemUrl")]
        public string ItemUrl { get; set; } // URL of program/watershed/site/visit

        [JsonProperty("values")]
        public List<MetricValueModel> Values { get; set; }

        public MetricInstanceModel()
        {
            Values = new List<MetricValueModel>();
        }
    }
}