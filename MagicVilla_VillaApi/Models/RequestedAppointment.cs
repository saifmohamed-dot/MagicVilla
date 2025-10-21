using MagicVilla_VillaApi.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaApi.Models
{
    public class RequestedAppointment : IEntity
    {
        [Required(ErrorMessage = "This Id Field Can't be Empty.")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "This Appointment Id Field Can't be Empty.")]
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "This ClientId Field Can't be Empty.")]
        public int ClientId { get; set; } // the client want to make an appointment .
        public int IsTaken { get; set; } = 0;
        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }
        [ForeignKey("ClientId")]
        public LocalUser Client {  get; set; } 
    }
}
