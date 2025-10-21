using MagicVilla_Web.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services;
using MagicVilla_Web.ViewModels;
using System.Security.Claims;

namespace MagicVilla_Web.Util
{
    public static class ViewModelsPropertiesAdjustment
    {
        public static void AppointmentImagesVmAdjustment(VillaAppointmentAndImagesCreateVM vm)
        {
            for (int i = 0; i < vm.Images.Count(); i++)
            {
                //var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(vm.Images[i].FileName);
                //var fullName = Path.Combine(rootPath, filename);
                vm.ImageCreateDtos.Add(new VillaPreviewImageCreateDto()
                {
                    ImageUrl = filename,
                    VillaId = vm.Appointments[0].VillaId
                });
            }
        }
    }
}
