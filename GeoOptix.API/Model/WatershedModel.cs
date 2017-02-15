using System;
using System.Collections.Generic;
using GeoOptix.API.Interface;
using Newtonsoft.Json;

namespace GeoOptix.API.Model
{
    public class WatershedModel : WatershedSummaryModel, IHasFolderModels, IHasFileModels, IHasFieldFolderModels, IHasMetricSchemaModels
    {
        [JsonProperty("sites")]
        public List<SiteSummaryModel> Sites { get; set; }

        [JsonProperty("folders")]
        public IEnumerable<FolderSummaryModel> Folders { get; set; }

        [JsonProperty("files")]
        public IEnumerable<FileSummaryModel> Files { get; set; }

        [JsonProperty("metricSchemas")]
        public IEnumerable<MetricSchemaModel> MetricSchemas { get; set; }

        [JsonProperty("fieldFolders")]
        public IEnumerable<FolderSummaryModel> FieldFolders { get; set; }

        [JsonProperty("sampledesigns")]
        public IEnumerable<StudyDesignModel> SampleDesigns { get; set; }

        public WatershedModel()
        {
            Sites = new List<SiteSummaryModel>();
        }
    }
}