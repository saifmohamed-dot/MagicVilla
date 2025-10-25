using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;

namespace MagicVilla_VillaApi.Repository
{
    public interface IRequestedAppointmentRepository : IRepository<RequestedAppointment>
    {
        Task<RequestedAppointment> UpdateAsync(RequestedAppointment requestedAppointment);
        Task BulkInsertRequstedAppointments(IEnumerable<RequestedAppointment> requests);
    }
}
