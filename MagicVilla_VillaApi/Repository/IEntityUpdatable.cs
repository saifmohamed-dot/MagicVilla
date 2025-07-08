namespace MagicVilla_VillaApi.Repository
{
    public interface IEntityUpdatable<T> where T : class , IEntity
    {
        void Update(T entity);
        // to Avoid Casting in the Controller ->
        IEntityUpdatable<T> GetUpdatable();
    }
}
