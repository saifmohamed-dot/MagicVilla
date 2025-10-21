using MagicVilla_VillaApi.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaApi.Models
{
    public class Appointment : IEntity
    {
        [Required(ErrorMessage = "This Id Field Can't be Empty.")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "This VillaId Field Can't be Empty.")]
        public int VillaId { get; set; }
        [Required(ErrorMessage = "This Date Field Can't be Empty.")]
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; } = true; // till it taken it will be false .
        [ForeignKey("VillaId")]
        public Villa Villa { get; set; }
        public ICollection<RequestedAppointment> RequestedAppointments { get; set; }
    }
}
