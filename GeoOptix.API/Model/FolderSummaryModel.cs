using GeoOptix.API.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GeoOptix.API.Model
{
    public class FolderSummaryModel : IHasUrl
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("parentUrl")]
        public string ParentUrl { get; set; }


        [JsonProperty("objectType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ObjectType ObjectType { get; set; }


        public FolderSummaryModel(string name, string url, string parentUrl, ObjectType objectType)
        {
            Name = name;
            Url = url;
            ParentUrl = parentUrl;
            ObjectType = objectType;
        }
    }
}