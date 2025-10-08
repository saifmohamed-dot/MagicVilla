using MagicVilla_Web.Dto;
using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services
{
    public class UserRepository : BaseService, IUserRepository
    {
        readonly string _baseUrl;
        public UserRepository(IHttpClientFactory httpClientFactory , IConfiguration config) : base(httpClientFactory)
        {
            _baseUrl = config.GetValue<string>("ServiceUrls:VillaAPI")!;
        }
        public Task<T> LoginAsync<T>(LoginRequestDto loginRequestDto)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIMethod = StaticUtil.APIMethod.POST,
                Url = _baseUrl + "/api/User/login",
                Data = loginRequestDto
                
            });
        }

        public Task<T> RegisterUserAsync<T>(UserRegistrationRequestDto userRegistrationRequestDto)
        {
            return SendAsync<T>(new APIRequest()
            {
                aPIMethod = StaticUtil.APIMethod.POST,
                Url = _baseUrl + "/api/User/register",
                Data = userRegistrationRequestDto
            });
        }
    }
}
