using MagicVilla_VillaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Repository
{
    public class RequestedAppointmentRepository : Repository<RequestedAppointment>, IRequestedAppointmentRepository
    {
        readonly DbContext _dbContext;
        public RequestedAppointmentRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BulkInsertRequstedAppointments(IEnumerable<RequestedAppointment> requests)
        {
            await _dbContext.Set<RequestedAppointment>().AddRangeAsync(requests);
            await SaveAsync();
        }

        public Task<RequestedAppointment> UpdateAsync(RequestedAppointment requestedAppointment)
        {
            // TODO : implement it after ui adjustments .
            throw new NotImplementedException();
        }
    }
}
