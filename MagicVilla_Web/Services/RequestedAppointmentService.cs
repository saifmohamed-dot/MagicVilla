using MagicVilla_Web.Models;
using MagicVilla_Web.ViewModels;

namespace MagicVilla_Web.Services
{
    public class RequestedAppointmentService : BaseService, IRequestedAppointmentService
    {
        readonly string _baseUrl;
        public RequestedAppointmentService(IHttpClientFactory httpClient , IConfiguration config) : base(httpClient)
        {
            _baseUrl = config.GetValue<string>("ServiceUrls:VillaAPI")!;
        }
        public Task<U> CreateBatch<U>(RequestListVM vm, string? authToken = null)
        {
            return SendAsync<U>(new APIRequest()
            {
                aPIMethod = StaticUtil.APIMethod.POST,
                Url = _baseUrl + "/api/RequestAppointment/Batch",
                Data = vm,
                Token = authToken ?? string.Empty
            });
        }
    }
}
