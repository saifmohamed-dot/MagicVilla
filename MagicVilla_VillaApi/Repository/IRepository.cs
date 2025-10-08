using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository
{
    // A Generic Crud Operation ->
    public interface IRepository<T> where T : class, IEntity
    {
        // return all rows unconditionally ->
        Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
        // return all rows that meet condition ->
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T , bool>> predicate , string? includeProperties = null);
        // find a Row by id -> 
        Task<T> GetByIdAsync(int id , bool tracking = true , string? incincludeProperties = null);
        // create a Row -> 
        Task<int> CreateAsync(T entity);
        // Delete 
        Task DeleteAsync(T entity);
        // a wrapper around db.SaveChangesAsync ->
        Task SaveAsync();
    }
}
