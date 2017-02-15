using System;
using System.Collections.Generic;
using System.Linq;
using GeoOptix.API.Interface;
using Newtonsoft.Json;

namespace GeoOptix.API.Model
{
    public class VisitModel : VisitSummaryModel, IHasFolderModels, IHasFileModels, IHasFieldFolderModels, IHasMetricSchemaModels
    {
        [JsonProperty("organizationName")]
        public string OrganizationName { get; private set; }

        [JsonProperty("hitchName")]
        public string HitchName { get; private set; }

        [JsonProperty("crewName")]
        public string CrewName { get; private set; }

        [JsonProperty("protocol")]
        public string Protocol { get; private set; }

        [JsonProperty("panel")]
        public string Panel { get; private set; }

        [JsonProperty("useOrder")]
        public int UseOrder { get; private set; }

        [JsonProperty("designUrl")]
        public string DesignUrl { get; private set; }

        [JsonProperty("tags")]
        public string[] Tags { get; private set; }

        [JsonProperty("folders")]
        public IEnumerable<FolderSummaryModel> Folders { get; set; }

        [JsonProperty("files")]
        public IEnumerable<FileSummaryModel> Files { get; set; }

        [JsonProperty("fieldFolders")]
        public IEnumerable<FolderSummaryModel> FieldFolders { get; set; }

        [JsonProperty("metricSchemas")]
        public IEnumerable<MetricSchemaModel> MetricSchemas { get; set; }

        [JsonProperty("measurements")]
        public IEnumerable<MeasurementSummaryModel> Measurements { get; set; }

        [JsonProperty("siteApiEndpoint")]
        public string SiteApiEndpoint { get; set; }

        [Obsolete] public VisitModel() { }

        public VisitModel(int id, string name, string url, string siteUrl, string status, DateTime? lastMeasurementChange, int? sampleYear, DateTime? sampleDate, DateTime? lastUpdated, 
                          string organizationName, string hitchName, string crewName, string protocol, string panel, int useOrder, string designUrl, string[] tags, 
                          IEnumerable<MeasurementSummaryModel> measurements, string designSiteUrl)
            : base(id, name, url, siteUrl, status, lastMeasurementChange, sampleYear, sampleDate, lastUpdated)
        {
            OrganizationName = organizationName;
            HitchName = hitchName;
            CrewName = crewName;
            Protocol = protocol;
            Panel = panel;
            UseOrder = useOrder;
            DesignUrl = designUrl;
            Tags = tags;
            Measurements = measurements;
            SiteApiEndpoint = designSiteUrl;
        }
    }
}