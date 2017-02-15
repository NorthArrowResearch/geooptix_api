using System.Collections.Generic;
using GeoOptix.API.Model;

namespace GeoOptix.API.Interface
{
    public interface IHasMetricSchemaModels
    {
        IEnumerable<MetricSchemaModel> MetricSchemas { get; set; }
    }
}