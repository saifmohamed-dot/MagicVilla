namespace MagicVilla_Web.Models
{
    public static class StaticUtil
    {
        public enum APIMethod
        {
            GET,
            POST, 
            PUT, 
            DELETE
        }
        public static string TokenName = "JwtToken";
    }
}
