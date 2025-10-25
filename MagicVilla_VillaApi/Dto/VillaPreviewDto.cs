using MagicVilla_VillaApi.Models;

namespace MagicVilla_VillaApi.Dto
{
    public class VillaPreviewDto
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string Address { get; set; }
        public string Details { get; set; }
        public UserDto Owner { get; set; }
        public ICollection<VillaPreviewAppointmentDto> Appointments { get; set; }
        public ICollection<VillaImagesPreviewDto> Images { get; set; }

    }
}
