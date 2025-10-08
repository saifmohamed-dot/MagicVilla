using MagicVilla_Web.Dto;
using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberServices
    {
        readonly string _baseUrl;
        public VillaNumberService(IHttpClientFactory httpClient , IConfiguration config) : base(httpClient) // pass base class dependencies 
        {
            _baseUrl = config.GetValue<string>("ServiceUrls:VillaAPI")!;
        } 
        public Task<U> CreateAsync<U>(VillaNumberCreateDto dto, string? authToken = null)
        {
            return SendAsync<U>(new APIRequest()
            {
                aPIMethod = StaticUtil.APIMethod.POST,
                Url = _baseUrl + "/api/VillaNumber/",
                Data = dto,
                Token = authToken ?? string.Empty
            });
        }

        public Task<U> DeleteAsync<U>(int id, string? authToken = null)
        {
            return SendAsync<U>(new APIRequest()
            {
                aPIMethod = StaticUtil.APIMethod.DELETE,
                Url = _baseUrl + $"/api/VillaNumber/{id}",
                Token = authToken ?? string.Empty
            });
        }

        public Task<U> GetAllAsync<U>(string? authToken = null)
        {
            return SendAsync<U>(new APIRequest()
            {
                aPIMethod = StaticUtil.APIMethod.GET,
                Url = _baseUrl + "/api/VillaNumber/",
                Token = authToken ?? string.Empty
            });
        }

        public Task<U> GetByIdAsync<U>(int id, string? authToken = null)
        {
            return SendAsync<U>(new APIRequest()
            {
                aPIMethod = StaticUtil.APIMethod.GET,
                Url = _baseUrl + $"/api/VillaNumber/{id}",
                Token = authToken ?? string.Empty
            });
        }

        public Task<U> UpdateAsync<U>(int id, VillaNumberUpdateDto dto, string? authToken = null)
        {
            return SendAsync<U>(new APIRequest()
            {
                aPIMethod = StaticUtil.APIMethod.PUT,
                Url = _baseUrl + $"/api/VillaNumber/{id}",
                Data = dto,
                Token = authToken ?? string.Empty
            });
        }
    }
}
