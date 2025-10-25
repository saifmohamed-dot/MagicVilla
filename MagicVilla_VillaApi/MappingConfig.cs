using AutoMapper;
using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;
namespace MagicVilla_VillaApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>().ReverseMap();
            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<VillaDto , VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();
            CreateMap<VillaNumber , VillaNumberDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreateDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDto>().ReverseMap();
            CreateMap<VillaNumberDto , VillaNumberUpdateDto>().ReverseMap();
            CreateMap<UserDto, LocalUser>().ReverseMap();
            CreateMap<AppointmentDto, Appointment>().ReverseMap();
            CreateMap<AppointmentCreateDto, Appointment>().ReverseMap();
            CreateMap<RequestedAppointment, RequestedAppointmentDto>().ReverseMap();
            CreateMap<RequestedAppointment , RequestedAppointmentCreateDto>().ReverseMap();
            CreateMap<VillaImagesPreviewCreateDto, VillaPreviewImages>().ReverseMap();
            CreateMap<VillaPreviewDto, Villa>().ReverseMap();
            CreateMap<VillaPreviewImages, VillaImagesPreviewDto>().ReverseMap();
            CreateMap<VillaPreviewRequestedAppointmentsDto, RequestedAppointment>().ReverseMap();
            CreateMap<VillaPreviewAppointmentDto, Appointment>().ReverseMap();
            CreateMap<VillaPreviewImages, VillaPreviewImageCreateDto>().ReverseMap();
            //CreateMap<RequestedAppointment, RequestAppointmentCreateDto>().ReverseMap();
        }
    }
}
