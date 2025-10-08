using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Dto
{
    public class VillaNumberUpdateDto
    {
        [Required(ErrorMessage = "This VillaNumber(id) Is MANDATORY!")]

        public int Id { get; set; } // it should'v be a villaNo , but for the sake of the IEntity Interface 
        [Required(ErrorMessage = "This VillaId(id) Is MANDATORY!")]
        public int VillaId { get; set; }
        public string SpecialDetail { get; set; } = string.Empty;
    }
}
