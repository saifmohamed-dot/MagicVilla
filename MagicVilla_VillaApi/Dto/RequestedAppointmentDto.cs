using MagicVilla_VillaApi.Models;
namespace MagicVilla_VillaApi.Dto
{
    public class RequestedAppointmentDto
    {
        
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int ClientId { get; set; } // the client want to make an appointment .
        public bool IsTaken { get; set; } = false;
        public Appointment Appointment { get; set; }
        public LocalUser Client { get; set; }
    }
}
