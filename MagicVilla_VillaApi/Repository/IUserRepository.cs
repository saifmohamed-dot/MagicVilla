using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;

namespace MagicVilla_VillaApi.Repository
{
    public interface IUserRepository
    {
        Task<LoginResponseDto> LoginUserAsync(LoginRequestDto loginRequestDto);
        Task<LocalUser> RegisterUserAsync(UserRegistrationRequestDto userRegistrationRequestDto);
        Task<LocalUser> GetUserByUserName(string userName);
        Task<List<LocalUser>> GetAllUsersAsync();
    }
}
