using MagicVilla_Web.Dto;

namespace MagicVilla_Web.Services
{
    public interface IUserRepository
    {
        Task<T> LoginAsync<T>(LoginRequestDto loginRequestDto);
        Task<T> RegisterUserAsync<T>(UserRegistrationRequestDto userRegistrationRequestDto);
    }
}
