using System.Net;

namespace GeoOptix.API
{
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Payload { get; set; }
    }
}