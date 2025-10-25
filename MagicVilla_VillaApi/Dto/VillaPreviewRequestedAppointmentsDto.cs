namespace MagicVilla_VillaApi.Dto
{
    public class VillaPreviewRequestedAppointmentsDto
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int ClientId { get; set; } // the client want to make an appointment .
        public int IsTaken { get; set; } = 0;
    }
}
