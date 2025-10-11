using MagicVilla_Web.Dto;
using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService , IVillaService
    {
        string _baseUrl { get; set; }
        public VillaService(IHttpClientFactory client , IConfiguration config) : base(client)
        {
            _baseUrl = config.GetValue<string>("ServiceUrls:VillaAPI")!;
        }

        public  Task<U> CreateAsync<U>(VillaCreateDto dto, string? authToken = null)
        {
            return SendAsync<U>(new APIRequest()
            {
                aPIMethod = StaticUtil.APIMethod.POST,
                Data = dto,
                Url = _baseUrl + "/api/VillaApi/",
                Token = authToken ?? string.Empty
            });
        }

        public Task<U> DeleteAsync<U>(int id, string? authToken = null)
        {
            return SendAsync<U>(new APIRequest()
            {
                aPIMethod = StaticUtil.APIMethod.DELETE,
                Url = _baseUrl + $"/api/VillaApi/{id}",
                Token = authToken ?? string.Empty
            });
        }

        public Task<U> GetAllAsync<U>(string? authToken = null)
        {
            return SendAsync<U>(new APIRequest()
            {
                aPIMethod = StaticUtil.APIMethod.GET,
                Url = _baseUrl + "/api/VillaApi/",
                Token = authToken ?? string.Empty
            });
        }

        public Task<U> GetByIdAsync<U>(int id, string? authToken = null)
        {
            return SendAsync<U>(new APIRequest()
            {
                aPIMethod = StaticUtil.APIMethod.GET,
                Url = _baseUrl + $"/api/VillaApi/{id}",
                Token = authToken ?? string.Empty
            });
        }

        public Task<U> UpdateAsync<U>(int id , VillaUpdateDto dto, string? authToken = null)
        {
            return SendAsync<U>(new APIRequest()
            {
                aPIMethod = StaticUtil.APIMethod.PUT,
                Data = dto,
                Url = _baseUrl + $"/api/VillaApi/{id}",
                Token = authToken ?? string.Empty
            });
        }

        public Task<U> GetAllAsync<U>(int pageNo, int pageSize, string? authToken = null)
        {
            return SendAsync<U>(new APIRequest()
            {
                aPIMethod = StaticUtil.APIMethod.GET,
                Url = _baseUrl + $"/api/VillaApi?pageNo={pageNo}&pageSize={pageSize}",
                Token = authToken ?? string.Empty
            });
        }
    }
}
