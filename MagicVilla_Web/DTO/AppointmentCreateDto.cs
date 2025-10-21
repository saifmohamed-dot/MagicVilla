using System.ComponentModel.DataAnnotations;
namespace MagicVilla_Web.DTO
{
    public class AppointmentCreateDto
    {
        [Required(ErrorMessage = "This VillaId Field Can't be Empty.")]
        public int VillaId { get; set; }
        [Required(ErrorMessage = "This Date Field Can't be Empty.")]
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; } = true; // till it taken it will be false .
    }
}
