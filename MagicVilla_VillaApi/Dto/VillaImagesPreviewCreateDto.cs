using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Dto
{
    public class VillaImagesPreviewCreateDto
    {
        [Required(ErrorMessage = "VillaId Required ")]
        public int VillaId {  get; set; }
        [Required(ErrorMessage = "Image Url Can Not Be Null")]
        public string ImageUrl { get; set; }
    }
}
