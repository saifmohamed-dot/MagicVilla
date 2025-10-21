using MagicVilla_Web.Dto;
using MagicVilla_Web.DTO;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.ViewModels
{
    public class VillaAppointmentAndImagesCreateVM
    {
        [Required(ErrorMessage = "Initial Appointment Needed")]
        public IList<AppointmentCreateDto> Appointments { get; set; }
        [Required(ErrorMessage = "Three Images Preview Needed")]
        public IList<IFormFile> Images { get; set; }
        public IList<VillaPreviewImageCreateDto> ImageCreateDtos { get; set; } = new List<VillaPreviewImageCreateDto>();
    }
}
