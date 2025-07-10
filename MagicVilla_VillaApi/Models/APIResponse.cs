using System.Data;
using System.Net;

namespace MagicVilla_VillaApi.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public object? Result { get; set; }
        public List<string>? Errors { get; set; }
        public void PopulateOnFail(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            IsSuccess = false;
        }
        public void PopulateOnSuccess(HttpStatusCode statusCode , object? result)
        {
            StatusCode = statusCode;
            IsSuccess = true;
            Result = result;
        }
    }
}
