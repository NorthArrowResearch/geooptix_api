using GeoOptix.API.Model;

namespace GeoOptix.API.Interface
{
    public interface IHasUrl
    {
        ObjectType ObjectType { get; }

        string Url { get; }
    }
}