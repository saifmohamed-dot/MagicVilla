using MagicVilla_VillaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Repository
{
    public class VillaRepository : Repository<Villa> , IVillaRepository
    {
        readonly DbContext _context;
        public VillaRepository(DbContext dbContext) 
        : base(dbContext)
        {
            _context = dbContext;
        }
        
        public async Task Update(Villa entity)
        {
            entity.DateUpdated = DateTime.Now;
            _context.Set<Villa>().Update(entity);
            await SaveAsync();
        }
    }
}
