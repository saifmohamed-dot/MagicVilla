using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Dto
{
    public class VillaDto
    {
        [Required(ErrorMessage = "ID For Villa Can't be Empty")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name For Villa Can't be Empty")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Details For Villa Can't be Empty")]
        public string Details { get; set; } = string.Empty;
        [Required(ErrorMessage = "Rate For Villa Can't be Empty")]
        [Range(0, 10, ErrorMessage = "Between 0 , 10")]
        public double Rate { get; set; }
        [Required(ErrorMessage = "sqft For Villa Can't be Empty")]
        public int Sqft { get; set; }
        [Required(ErrorMessage = "Occupancy For Villa Can't be Empty")]
        public int Occupancy { get; set; }
        [Required(ErrorMessage = "Image For Villa Can't be Empty")]
        public string ImageUrl { get; set; } = string.Empty;
        [Required(ErrorMessage = "Amentiy For Villa Can't be Empty")]
        public string Amentiy { get; set; } = string.Empty;
    }
}
