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

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, string? includeProperties = null)
        {
            IQueryable<T> query = _set!;
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(',' , StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string? includeProperties)
        {
            IQueryable<T> query = _set!;
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(int pageNo, int pageSize, bool tracking , string? includeProperties = null)
        {
            IQueryable<T> query = _set!;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            if(!tracking)
            {
                query = query.AsNoTracking();
            }
            return await query.Skip(pageNo * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id , bool tracking = true , string? includeProperties = null)
        {
            IQueryable<T> query = _set!;
            if (!string.IsNullOrEmpty(includeProperties)) 
            {
                foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            if(!tracking)
            {
                query = query.AsNoTracking();
            }
           return await query.Where(e => e.Id == id).FirstOrDefaultAsync();
        }
        public async Task SaveAsync()
        {
            await _context!.SaveChangesAsync();
        }
    }
}
