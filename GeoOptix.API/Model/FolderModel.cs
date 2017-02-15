using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GeoOptix.API.Model
{
    public class FolderModel : FolderSummaryModel
    {
        [JsonProperty("dateCreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("dateModified")]
        public DateTime DateModified { get; set; }

        [JsonProperty("published")]
        public bool Published { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("files")]
        public IEnumerable<FileSummaryModel> Files { get; set; }


        public FolderModel(string name, string url, string parentUrl, ObjectType objectType, DateTime dateCreated, DateTime dateModified, bool published, bool locked, IEnumerable<FileSummaryModel> files) 
            : base(name, url, parentUrl, objectType)
        {
            DateCreated = dateCreated;
            DateModified = dateModified;
            Published = published;
            Locked = locked;
            Files = files;
        }
    }    
    
    
    public class FieldFolderModel : FolderSummaryModel
    {
        [JsonProperty("files")]
        public IEnumerable<FileSummaryModel> FieldFiles { get; set; }


        public FieldFolderModel(string name, string url, string parentUrl, ObjectType objectType, IEnumerable<FileSummaryModel> files) : base(name, url, parentUrl, objectType)
        {
            FieldFiles = files;
        }
    }
}