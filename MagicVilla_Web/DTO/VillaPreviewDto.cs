
namespace MagicVilla_Web.Dto
{
    public class VillaPreviewDto
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string Address { get; set; }
        public UserDto Owner { get; set; }
        public ICollection<VillaPreviewAppointmentDto> Appointments { get; set; }
        public ICollection<VillaImagesPreviewDto> Images { get; set; }

    }
}
