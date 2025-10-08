using MagicVilla_VillaApi.Models;

namespace MagicVilla_VillaApi.Repository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task Update(VillaNumber villaNumber);
    }
}
