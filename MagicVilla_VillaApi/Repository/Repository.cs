using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class , IEntity
    {
        readonly DbContext? _context;
        readonly DbSet<T>? _set;
        public Repository(DbContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }
        public async Task<int> CreateAsync(T entity)
        {
            _set!.Add(entity);
            await SaveAsync();
            return await _set!.MaxAsync(e => e.Id);
        }

        public async Task DeleteAsync(T entity)
        {
            _set!.Remove(entity);
            await SaveAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set!.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _set!.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id , bool tracking = true)
        {
            if (tracking)
                return await _set!.Where(e => e.Id == id).FirstOrDefaultAsync();
            return await _set!.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }
        public async Task SaveAsync()
        {
            await _context!.SaveChangesAsync();
        }
    }
}
