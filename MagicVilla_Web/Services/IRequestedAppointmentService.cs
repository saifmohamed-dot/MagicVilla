using MagicVilla_Web.DTO;
using MagicVilla_Web.ViewModels;

namespace MagicVilla_Web.Services
{
    public interface IRequestedAppointmentService
    {
        Task<U> CreateBatch<U>(RequestListVM vm, string? authToken = null);
    }
}
