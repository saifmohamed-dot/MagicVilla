using MagicVilla_VillaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber> , IVillaNumberRepository
    {
        readonly DbContext _dbContext;
        public VillaNumberRepository(DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Update(VillaNumber number)
        {
            number.DateUpdated = DateTime.Now;
            _dbContext.Set<VillaNumber>().Update(number);
            await SaveAsync();
        }
    }
}
