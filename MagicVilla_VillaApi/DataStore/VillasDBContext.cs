using MagicVilla_VillaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.DataStore
{
    public class VillasDBContext : DbContext
    {
        public VillasDBContext(DbContextOptions<VillasDBContext> option)
            :base(option) 
        { 
        }
        public DbSet<LocalUser> Users { get; set; }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<RequestedAppointment> RequestedAppointments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "VillaDia",
                    Details = "this Is a pritty good villa",
                    Rate = 4.3,
                    Sqft = 3003,
                    Occupancy = 5,
                    ImageUrl = "https://localhost/dev/tech",
                    Amentiy = "very good",
                    DateCreated = new DateTime(2025, 7, 5),
                    DateUpdated = new DateTime(2025, 7, 5)
                },
                new Villa
                {
                    Id = 2,
                    Name = "VillaCont",
                    Details = "this Is a meduim good villa",
                    Rate = 3.4,
                    Sqft = 2500,
                    Occupancy = 3,
                    ImageUrl = "https://localhost/dev/tech",
                    Amentiy = "meduim",
                    DateCreated = new DateTime(2025, 7, 5),
                    DateUpdated = new DateTime(2025, 7, 5)
                },
                new Villa
                {
                    Id = 3,
                    Name = "TalaVilla",
                    Details = "this Is a bad villa",
                    Rate = 2.1,
                    Sqft = 1500,
                    Occupancy = 5,
                    ImageUrl = "https://localhost/dev/tech",
                    Amentiy = "bad",
                    DateCreated = new DateTime(2025, 7, 5),
                    DateUpdated = new DateTime(2025, 7, 5)
                },
                new Villa
                {
                    Id = 4,
                    Name = "CockedVilla",
                    Details = "this Is a Smoked villa",
                    Rate = 3.3,
                    Sqft = 1000,
                    Occupancy = 5,
                    ImageUrl = "https://localhost/dev/tech",
                    Amentiy = "very Smoked",
                    DateCreated = new DateTime(2025, 7, 5),
                    DateUpdated = new DateTime(2025, 7, 5)
                }
            );
        }
    }
}
