using System.Collections.Generic;
using GeoOptix.API.Model;

namespace GeoOptix.API.Interface
{
    public interface IHasFolderModels
    {
        IEnumerable<FolderSummaryModel> Folders { get; set; }
    }


    public interface IHasFieldFolderModels
    {
        IEnumerable<FolderSummaryModel> FieldFolders { get; set; }
    }
}