using System.Net;
using System.Text.Json;

namespace Rentering.WebAPI.Exceptions
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

        public string Message { get; set; } = "An unexpected error occurred.";

        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
