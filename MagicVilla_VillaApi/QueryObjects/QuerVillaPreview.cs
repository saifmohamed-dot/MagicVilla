
using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;
using Microsoft.EntityFrameworkCore;
namespace MagicVilla_VillaApi.QueryObjects
{
    public static class QuerVillaPreview
    {
        public static IQueryable<Villa> GetVillaPreviewDtoQueryable(this IQueryable<Villa> villas
            , int villaId, int clientId)
        {
            return villas.AsNoTracking().Include(v => v.Appointments.Where(ap => ap.IsAvailable))
                            .ThenInclude(ap => ap.RequestedAppointments.Where(req => req.ClientId == clientId))
                        .Include(v => v.Owner)
                        .Include(v => v.Images)
                        .Where(v => v.Id == villaId);
        }
        
    }
}
