using GeoOptix.API.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GeoOptix.API.Model
{
    public class ProgramSummaryModel : IHasUrl
    {
        [JsonProperty("id", Order = -1)]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("objectType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ObjectType ObjectType { get { return ObjectType.Program; } }
        [JsonProperty("mrprogramid")]
        public int? MRProgramID { get; set; }

        public ProgramSummaryModel() { }

    }
}