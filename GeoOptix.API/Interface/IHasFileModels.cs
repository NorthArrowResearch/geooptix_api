using System.Collections.Generic;
using GeoOptix.API.Model;

namespace GeoOptix.API.Interface
{
    public interface IHasFileModels
    {
        IEnumerable<FileSummaryModel> Files { get; set; }
    }
}