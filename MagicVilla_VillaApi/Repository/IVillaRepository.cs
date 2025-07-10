using MagicVilla_VillaApi.Models;

namespace MagicVilla_VillaApi.Repository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task Update(Villa villa);
    }
}
