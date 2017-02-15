using System.Collections.Generic;
using GeoOptix.API.Interface;
using Newtonsoft.Json;

namespace GeoOptix.API.Model
{
    public class ProgramModel : ProgramSummaryModel, IHasFolderModels, IHasFileModels, IHasFieldFolderModels, IHasMetricSchemaModels
    {
        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("watersheds")]
        public List<WatershedSummaryModel> Watersheds { get; set; }

        [JsonProperty("folders")]
        public IEnumerable<FolderSummaryModel> Folders { get; set; }

        [JsonProperty("files")]
        public IEnumerable<FileSummaryModel> Files { get; set; }

        [JsonProperty("metricSchemas")]
        public IEnumerable<MetricSchemaModel> MetricSchemas { get; set; }

        [JsonProperty("fieldFolders")]
        public IEnumerable<FolderSummaryModel> FieldFolders { get; set; }

        [JsonProperty("programApiEndpoint")]
        public string ProgramApiEndpoint { get; set; }

        public ProgramModel()
        {
            Watersheds = new List<WatershedSummaryModel>();
        }
    }
}