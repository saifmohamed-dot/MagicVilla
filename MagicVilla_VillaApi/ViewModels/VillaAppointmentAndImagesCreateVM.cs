using MagicVilla_VillaApi.Dto;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.ViewModels
{
    public class VillaAppointmentAndImagesCreateVM
    {
        [Required(ErrorMessage = "Initial Appointment Needed")]
        public IList<AppointmentCreateDto> Appointments { get; set; }
        [Required(ErrorMessage = "Images Required")]
        public IList<VillaPreviewImageCreateDto> ImageCreateDtos { get; set; }
    }
}
