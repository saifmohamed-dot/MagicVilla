using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Dto
{
    public class UserRegistrationRequestDto
    {
        [Required(ErrorMessage = "This Field Can't Be Empty ")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "This Field Can't Be Empty ")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "This Field Can't Be Empty ")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword {  get; set; } = string.Empty;
        public string Role {  get; set; } = "Customer";
    }
}
