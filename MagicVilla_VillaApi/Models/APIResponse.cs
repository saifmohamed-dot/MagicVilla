using System.Data;
using System.Net;

namespace MagicVilla_VillaApi.Models
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
        public void PopulateOnFail(HttpStatusCode statusCode , List<string>? errors = null)
        {
            StatusCode = statusCode;
            IsSuccess = false;
            if (errors != null)
            {
                Errors = errors;
            }
        }
        public void PopulateOnSuccess(HttpStatusCode statusCode , object? result)
        {
            StatusCode = statusCode;
            IsSuccess = true;
            Result = result;
        }
    }
}
