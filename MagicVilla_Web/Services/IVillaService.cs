using MagicVilla_Web.Dto;

namespace MagicVilla_Web.Services
{
    public interface IVillaService
    {
        Task<U> GetByIdAsync<U>(int id, string? authToken = null);
        Task<U> GetAllAsync<U>(string? authToken = null);
        Task<U> CreateAsync<U>(VillaCreateDto dto, string? authToken = null);
        Task<U> UpdateAsync<U>(int id , VillaUpdateDto dto, string? authToken = null);
        Task<U> DeleteAsync<U>(int id, string? authToken = null);
    }
}
