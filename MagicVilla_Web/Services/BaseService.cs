using MagicVilla_Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse ApiResponse { get; set; }
        IHttpClientFactory _httpClient;
        public BaseService(IHttpClientFactory client)
        {
            ApiResponse = new();
            _httpClient = client;
        }
        public async Task<T> SendAsync<T>(APIRequest request)
        {
            HttpClient client = _httpClient.CreateClient("VillaApi");
            HttpRequestMessage message = new HttpRequestMessage();
            message.RequestUri = new Uri(request.Url);
            Console.WriteLine(message.RequestUri.ToString());
            message.Headers.Add("Accpet", "application/json");
            // check if there is a token passed ->
            if (request.Token != null)
            {
                Console.WriteLine($"toke : {request.Token}");
                message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer" , request.Token);
            }
            // there is data to be send to the api ->
            try
            {
                if (request.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data)
                        , Encoding.UTF8, "application/json");
                }
                switch (request.aPIMethod)
                {
                    case StaticUtil.APIMethod.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case StaticUtil.APIMethod.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case StaticUtil.APIMethod.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage resMessage = await client.SendAsync(message);
                string content = await resMessage.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<T>(content);
                return response;
            }
            catch (Exception ex)
            {
                APIResponse apiresponse = new APIResponse();
                apiresponse.PopulateOnFail(System.Net.HttpStatusCode.BadRequest);
                apiresponse.Errors.Add(ex.Message);
                var res = JsonConvert.SerializeObject(apiresponse);
                var resposne = JsonConvert.DeserializeObject<T>(res);
                return resposne;
            }


        }
    }
}
