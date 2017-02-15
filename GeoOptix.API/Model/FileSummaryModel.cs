
using System;
using GeoOptix.API.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GeoOptix.API.Model
{
    public class FileSummaryModel : IHasUrl
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("folderUrl")]
        public string FolderUrl { get; set; }

        [JsonProperty("downloadUrl")]
        public string DownloadUrl { get; set; }

        [JsonProperty("objectType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ObjectType ObjectType { get; private set; }


        [Obsolete] public FileSummaryModel() { }


        public FileSummaryModel(string name, string description, string folderUrl, string url)
        {
            Name = name;
            Description = description;
            FolderUrl = folderUrl;
            Url = url;
            DownloadUrl = url + "?download";
            ObjectType = ObjectType.File;
        }
    }
}