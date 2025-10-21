using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Dto
{
    public class VillaPreviewImageCreateDto
    {
        [Required(ErrorMessage = "Image Url Can not Be Empty.")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Villa Id Can not Be Empty.")]
        public int VillaId { get; set; }
    }
}
