using AutoMapper;
using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.QueryObjects
{
    public static class QueryOwnerVilla
    {
        public static IQueryable<OwnerVillaDto> GetOwnerVillaDtoQuerable(this IQueryable<Villa> villas , int id)
        {
            return villas.AsNoTracking().Where(v => v.OwnerId == id).Select(v => new OwnerVillaDto()
            {
                Id = v.Id,
                Name = v.Name,
                Details = v.Details,
                ImageUrl = v.ImageUrl,
                Price = v.Price,
                Appointments = v.Appointments.Where(app => app.IsAvailable == true).Select(app => new AppointmentDto()
                {
                    Id = app.Id,
                    Date = app.Date,
                })
            });
        }
    }
}
