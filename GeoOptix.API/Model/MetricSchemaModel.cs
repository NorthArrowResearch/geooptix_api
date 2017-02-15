using System.Collections.Generic;
using GeoOptix.API.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GeoOptix.API.Model
{
    public class MetricSchemaModel : IHasUrl
    {
        public const string SCHEMA_PUBLISHED_STRING = "SchemaPublished";

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("published")]
        public bool Published { get; set; }

        [JsonProperty("objectType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ObjectType ObjectType { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("attributes")]
        public List<MetricAttributeModel> Attributes { get; set; }

        [JsonProperty("instances")]
        public List<MetricInstanceSummaryModel> Instances { get; set; }


        public MetricSchemaModel(string name, bool locked, bool published, ObjectType objectType, string url, List<MetricAttributeModel> attributes, List<MetricInstanceSummaryModel> instances)
        {
            Name = name;
            Locked = locked;
            Published = published;
            ObjectType = objectType;
            Url = url;
            Attributes = attributes;
            Instances = instances;
        }
    }
}