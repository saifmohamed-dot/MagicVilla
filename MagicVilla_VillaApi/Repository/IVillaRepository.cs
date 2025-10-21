using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Repository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task Update(Villa villa);
        Task<Villa> GetVillaPreviewQuery(int villaId, int clientId);
        Task AddRelativeDataToVillaCreateAsync(VillaAppointmentAndImagesCreateVM vm);
    }
}
