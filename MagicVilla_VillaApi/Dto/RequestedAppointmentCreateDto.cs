
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Dto
{
    public class RequestedAppointmentCreateDto
    {
        
        [Required(ErrorMessage = "This Appointment Id Field Can't be Empty.")]
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "This ClientId Field Can't be Empty.")]
        public int ClientId { get; set; } // the client want to make an appointment .
        public int IsTaken { get; set; } = 0;
    }
}
