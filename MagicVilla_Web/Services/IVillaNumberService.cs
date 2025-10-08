using MagicVilla_Web.Dto;

namespace MagicVilla_Web.Services
{
    public interface IVillaNumberServices
    {
        Task<U> GetByIdAsync<U>(int id , string? authToken = null);
        Task<U> GetAllAsync<U>(string? authToken = null);
        Task<U> CreateAsync<U>(VillaNumberCreateDto dto , string? authToken = null);
        Task<U> UpdateAsync<U>(int id, VillaNumberUpdateDto dto , string? authToken = null);
        Task<U> DeleteAsync<U>(int id, string? authToken = null);
    }
}
