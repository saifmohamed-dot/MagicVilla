using MagicVilla_VillaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Repository
{
    public class VillaRepository : Repository<Villa> , IEntityUpdatable<Villa>
    {
        public VillaRepository(DbContext dbContext) 
        : base(dbContext) {  }
        // returning the current object with the IEntityUpdatable<Villa> referencing it ->
        // so we could referencing our own update without casting in the controller ->
        public IEntityUpdatable<Villa> GetUpdatable() => this;

        public void Update(Villa entity)
        {
            
        }
    }
}
