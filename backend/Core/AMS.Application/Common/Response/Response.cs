using System.Net;
using System.Text.Json.Serialization;

namespace AMS.Application.Common.Response
{
    public partial class Response<T>
    {

        public Response()
        {
            StatusCode = HttpStatusCode.OK;
        }

        public Response(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        public Response(HttpStatusCode statusCode, string? message)
        {
            StatusCode = statusCode;
            Message = message;
        }
        public Response(HttpStatusCode statusCode, T? data, string? message = null)
        {
            StatusCode = statusCode;
            Data = data;
            Message = message;
        }

        public string Status
        {
            get
            {
                var statusCodeValue = (int)StatusCode;

                string? value = statusCodeValue switch
                {
                    >= 200 and < 400 => Enum.GetName(enStatusType.Success), // 2xx range
                    >= 400 and < 500 => Enum.GetName(enStatusType.Error), // 4xx range for client errors
                    >= 500 and < 600 => Enum.GetName(enStatusType.Fail), // 5xx range for server errors
                    _ => throw new Exception($"Invalid Status code {statusCodeValue}") // Default to Error for any unhandled Status codes
                };

                return value!;
            }
        }
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
