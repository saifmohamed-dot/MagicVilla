using MagicVilla_VillaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Repository
{
    public class RequestedAppointmentRepository : Repository<RequestedAppointment>, IRequestedAppointmentRepository
    {
        public RequestedAppointmentRepository(DbContext dbContext) : base(dbContext) { }

        

        public Task<RequestedAppointment> UpdateAsync(RequestedAppointment requestedAppointment)
        {
            // TODO : implement it after ui adjustments .
            throw new NotImplementedException();
        }
    }
}
