using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Dto
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "This Field Can't Be Empty")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "This Field Can't Be Empty")]
        public string Password { get; set; } = string.Empty;
    }
}
