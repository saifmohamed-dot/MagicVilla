using MagicVilla_VillaApi.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaApi.Models
{
    public class VillaNumber : IEntity
    {
        [Key , DatabaseGenerated(DatabaseGeneratedOption.None)]
        // the id (villa number) will be a mandatory insertion from the user ->
        public int Id { get; set; } // it should'v be a villaNo , but for the sake of the IEntity Interface 
        public string SpecialDetail { get; set; } = string.Empty;
        [ForeignKey("Villa")]
        public int VillaId;
        public Villa Villa { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}
