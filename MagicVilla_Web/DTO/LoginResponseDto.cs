using MagicVilla_Web.Models;

namespace MagicVilla_Web.Dto
{
    public class LoginResponseDto
    {
        public UserDto? User { get; set; } // could be null if login failed .
        public string Token { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        List<string>? _errors; // could be null if login successed .
        public List<string> Errors
        {
            get => _errors ??= new List<string>();
            set => _errors = value;
        }
    }
}
