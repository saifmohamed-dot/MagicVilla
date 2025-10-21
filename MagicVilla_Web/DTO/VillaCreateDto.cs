using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Dto
{
    public class VillaCreateDto
    {
        public int OwnerId { get; set; } // will be populated with the currently logged user .
        [Required(ErrorMessage = "Name For Villa Can't be Empty")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Details For Villa Can't be Empty")]
        public string Details { get; set; } = string.Empty;
        [Required(ErrorMessage = "Rate For Villa Can't be Empty")]
        [Range(0 , 10 , ErrorMessage = "Between 0 , 10")]
        public double Rate { get; set; }
        [Required(ErrorMessage = "sqft For Villa Can't be Empty")]
        [Range(400 , 6000 , ErrorMessage = "sqft has to be between 400 , 6000")]
        public int Sqft { get; set; }
        [Required(ErrorMessage = "Occupancy For Villa Can't be Empty")]
        public int Occupancy { get; set; }
        // that will be populated when image 
        public string ImageUrl { get; set; } = string.Empty;
        [Required(ErrorMessage = "You Have To Upload A Profile Image For Your Villa")]
        [Display(Name = "Villa Profile Picture")]
        public IFormFile UploadedImage { get; set; }
        [Required(ErrorMessage = "Amentiy For Villa Can't be Empty")]
        public string Amentiy { get; set; } = string.Empty;
        [Required]
        public float Price { get; set; } = 100.20f;
        [Required]
        public string Address { get; set; } = "Cairo Egypt";
    }
}
