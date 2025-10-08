using static MagicVilla_Web.Models.StaticUtil;

namespace MagicVilla_Web.Models
{
    public class APIRequest
    {
        public APIMethod aPIMethod { get; set; } = APIMethod.GET;
        public string Url { get; set; } = string.Empty;
        public object? Data { get; set; }
        public string Token;
    }
}
