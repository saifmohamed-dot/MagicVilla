using MagicVilla_VillaApi.Models;

namespace MagicVilla_VillaApi.Repository
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        // TODO : implement the update function 
        // TODO : get appointments based on villaId
        // TODO : Get appointments based on availabilty .
        Task UpdateAsync(Appointment appointment);
        Task BuilkInsertAppointment(IEnumerable<Appointment> appointments);
    }
}
