using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace GeoOptix.API.Model
{
    public class MeasurementSummaryModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("parentUrl")]
        public string ParentUrl { get; set; }

        [JsonProperty("objectType")]
        public string ObjectType { get { return "MeasurementType"; } }


        public MeasurementSummaryModel(string name, string url, string parentUrl)
        {
            Name = name;
            Url = url;
            ParentUrl = parentUrl;
        }
    }


    public class MeasurementModel<T> : MeasurementSummaryModel
    {
        [JsonProperty("values")]
        public IEnumerable<MeasValueModel<T>> MeasValues { get; set; }

        public MeasurementModel(string name, string url, string parentUrl, IEnumerable<MeasValueModel<T>> measValues) : base(name, url, parentUrl)
        {
            MeasValues = measValues;
        }
    }


    public class MeasValueModel<T>
    {
        [JsonProperty("value")]
        public T Measurement { get; set; }

        [JsonProperty("qaDecision")]
        public string Decision { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("objectType")]
        public string ObjectType { get { return "Measurement"; } }

        
        [JsonProperty("measurementType")]
        public string MeasurementType { get; set; }


        public MeasValueModel(T measurement, string measType, string decision, string note)
        {
            Measurement = measurement;
            MeasurementType = measType;
            Decision = decision;
            Note = note;
        }
    }
}