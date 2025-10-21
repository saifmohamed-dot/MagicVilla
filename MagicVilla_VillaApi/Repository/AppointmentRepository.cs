using MagicVilla_VillaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Repository
{
    public class AppointmentRepository : Repository<Appointment> , IAppointmentRepository
    {
        readonly DbContext _dbContext;
        public AppointmentRepository(DbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task BuilkInsertAppointment(IEnumerable<Appointment> appointments)
        {
            await _dbContext.Set<Appointment>().AddRangeAsync(appointments);
            await _dbContext.SaveChangesAsync();
        }

        // TODO : implement the update function .
        public Task UpdateAsync(Appointment appointment)
        {
            // TODO : Implementing it after ui adjustments .
            throw new NotImplementedException();
        }
    }
}
