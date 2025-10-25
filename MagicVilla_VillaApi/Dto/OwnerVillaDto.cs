namespace MagicVilla_VillaApi.Dto
{
    public class OwnerVillaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string ImageUrl { get; set; }
        public float Price { get; set; }
        public IEnumerable<AppointmentDto> Appointments { get; set; }
    }
}
