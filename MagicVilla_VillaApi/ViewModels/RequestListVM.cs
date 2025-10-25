using MagicVilla_VillaApi.Dto;
namespace MagicVilla_VillaApi.ViewModels
{
    public class RequestListVM
    {
        public IList<RequestedAppointmentCreateDto> Requests { get; set; }
    }
}
