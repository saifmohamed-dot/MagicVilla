using MagicVilla_VillaApi.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaApi.Models
{
    public class VillaPreviewImages : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Image Url Can not Be Empty.")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Villa Id Can not Be Empty.")]
        public int VillaId { get; set; }
        //public Villa Villa { get; set; }
    }
}
