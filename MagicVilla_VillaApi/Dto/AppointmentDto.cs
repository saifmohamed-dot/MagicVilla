using MagicVilla_VillaApi.Models;
namespace MagicVilla_VillaApi.Dto
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int VillaId { get; set; }
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; } = true; // till it taken it will be false .
        public Villa Villa { get; set; }
    }
}
