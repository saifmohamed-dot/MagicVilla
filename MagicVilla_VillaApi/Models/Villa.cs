using MagicVilla_VillaApi.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaApi.Models
{
    public class Villa : IEntity /* To Make The ID Property Abstract */
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int OwnerId { get; set; } = 1;
        [ForeignKey("OwnerId")]
        public LocalUser Owner { get; set; }
        [Required]
        public float Price { get; set; } = 100.20f;
        [Required]
        public string Address { get; set; } = "Cairo Egypt";

        public string Details { get; set; } = string.Empty;
        public double Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Amentiy { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public ICollection<VillaPreviewImages> Images { get; set; }
        public ICollection<Appointment> Appointments { get;set; }
    }
}
