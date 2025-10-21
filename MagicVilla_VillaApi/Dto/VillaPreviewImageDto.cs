using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Dto
{
    public class VillaPreviewImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int VillaId { get; set; }
    }
}
