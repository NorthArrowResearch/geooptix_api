using System;
using GeoOptix.API.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GeoOptix.API.Model
{
    public class VisitSummaryModel : IHasUrl
    {
        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("url")]
        public string Url { get; private set; }

        [JsonProperty("siteUrl")]
        public string SiteUrl { get; private set; }

        [JsonProperty("lastMeasurementChange")]
        public DateTime? LastMeasurementChange { get; private set; }

        [JsonProperty("status")]
        public string Status { get; private set; }

        [JsonProperty("sampleYear")]
        public int? SampleYear { get; private set; }

        [JsonProperty("sampleDate")]
        public DateTime? SampleDate { get; private set; }

        [JsonProperty("lastUpdated")]
        public DateTime? LastUpdated { get; private set; }

        [JsonProperty("objectType")]
        [JsonConverter(typeof (StringEnumConverter))]
        public ObjectType ObjectType
        {
            get { return ObjectType.Visit; }
        }


        [Obsolete] public VisitSummaryModel() { }

        public VisitSummaryModel(int id, string name, string url, string siteUrl, string status, DateTime? lastMeasurementChange, int? sampleYear, DateTime? sampleDate, DateTime? lastUpdated)
        {
            Id = id;
            Name = name;
            Url = url;
            LastMeasurementChange = lastMeasurementChange;
            SiteUrl = siteUrl;
            Status = status;
            SampleYear = sampleYear;
            SampleDate = sampleDate;
            LastUpdated = lastUpdated;
        }
    }
}