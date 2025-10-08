using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Dto
{
    public class VillaNumberCreateDto
    {
        [Required(ErrorMessage = "This VillaNumber(id) Is MANDATORY!")]
        public int Id { get; set; } // it should'v be a villaNo , but for the sake of the IEntity Interface 
        [Required(ErrorMessage = "This VillaId Is MANDATORY!")]
        public int VillaId { get; set; }
        public string SpecialDetail { get; set; } = string.Empty;
    }
}
