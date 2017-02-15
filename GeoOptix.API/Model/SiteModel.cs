using System;
using System.Collections.Generic;
using GeoOptix.API.Interface;
using Newtonsoft.Json;

namespace GeoOptix.API.Model
{
    public class SiteModel : SiteSummaryModel, IHasFolderModels, IHasFileModels, IHasFieldFolderModels, IHasMetricSchemaModels
    {
        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("datum")]
        public string Datum { get { return "WGS84"; } }

        [JsonProperty("visits")]
        public List<VisitSummaryModel> Visits { get; set; }

        [JsonProperty("folders")]
        public IEnumerable<FolderSummaryModel> Folders { get; set; }

        [JsonProperty("files")]
        public IEnumerable<FileSummaryModel> Files { get; set; }

        [JsonProperty("metricSchemas")]
        public IEnumerable<MetricSchemaModel> MetricSchemas { get; set; }

        [JsonProperty("fieldFolders")]
        public IEnumerable<FolderSummaryModel> FieldFolders { get; set; }

        // We need a parameterless constructor, sadly
        public SiteModel()
        {
            Visits = new List<VisitSummaryModel>();
        }


        public SiteModel(int id, string name, string url, string watershedUrl, string latitude, string longitude, string locale)
        {
            Visits = new List<VisitSummaryModel>();

            Id = id;
            Name = name;
            Url = url;
            WatershedUrl = watershedUrl;
            Latitude = latitude;
            Longitude = longitude;
            Locale = locale;
        }
    }
}