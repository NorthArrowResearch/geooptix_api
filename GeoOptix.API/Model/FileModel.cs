using System;
using Newtonsoft.Json;

namespace GeoOptix.API.Model
{
    public class FileModel : FileSummaryModel
    {
        [JsonProperty("size")]
        public long Size { get; private set; }

        [JsonProperty("dateUploaded")]
        public DateTime DateUploaded { get; private set; }

        [JsonProperty("hash")]
        public string Hash { get; private set; }
        

        [Obsolete] public FileModel() { }


        public FileModel(string name, string description, string folderUrl, string fileUrl, long size, DateTime dateUploaded, string hash)
            : base(name, description, folderUrl, fileUrl)
        {
            Size = size;
            DateUploaded = dateUploaded;
            Hash = hash;
        }
    }
}