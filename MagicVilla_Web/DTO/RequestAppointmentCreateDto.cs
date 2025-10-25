namespace MagicVilla_Web.DTO
{
    public class RequestAppointmentCreateDto
    {
        // i'll validate this with fluentValidation .
        public int AppointmentId { get; set; }
        public int ClientId { get; set; } // the client want to make an appointment .
        public int IsTaken { get; set; } = 0;
    }
}
