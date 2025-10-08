using System.Data;
using System.Net;

namespace MagicVilla_Web.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public object? Result { get; set; }
        List<string>? _errors;
        public List<string> Errors
        {
            get
            {
                if (_errors == null)
                {
                    _errors = new List<string>();
                }
                return _errors;
            }
            set
            {
                _errors = value;
            }
        }
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
