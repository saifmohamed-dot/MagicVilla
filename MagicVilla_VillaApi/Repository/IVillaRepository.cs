using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task Update(Villa villa);
        Task<Villa> GetVillaPreviewQuery(int villaId, int clientId);
        Task AddRelativeDataToVillaCreateAsync(VillaAppointmentAndImagesCreateVM vm);
        Task<IEnumerable<OwnerVillaDto>> GetOwnerVillas(int id);
    }
}
