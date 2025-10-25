using AutoMapper;
using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.QueryObjects;
using MagicVilla_VillaApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository
{
    public class VillaRepository : Repository<Villa> , IVillaRepository
    {
        readonly DbContext _context;
        readonly IAppointmentRepository _appRepo;
        readonly IVillaPreviewImagesRepository _imgRepo;
        readonly IMapper _mapper;
        public VillaRepository(DbContext dbContext 
            , IAppointmentRepository appRepo 
            , IVillaPreviewImagesRepository imgRepo
            , IMapper mapper) 
        : base(dbContext)
        {
            _context = dbContext;
            _appRepo = appRepo;
            _imgRepo = imgRepo;
            _mapper = mapper;
        }
        
        public async Task Update(Villa entity)
        {
            entity.DateUpdated = DateTime.Now;
            _context.Set<Villa>().Update(entity);
            await SaveAsync();
        }
        public async Task<Villa?> GetVillaPreviewQuery(int villaId , int clientId)
        {
            return await _context.Set<Villa>().GetVillaPreviewDtoQueryable(villaId, clientId).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<OwnerVillaDto>> GetOwnerVillas(int id)
        {
            return await _context.Set<Villa>().GetOwnerVillaDtoQuerable(id).ToListAsync();
        }
        public async Task AddRelativeDataToVillaCreateAsync(VillaAppointmentAndImagesCreateVM vm)
        {
            await _appRepo.BuilkInsertAppointment(_mapper.Map<IEnumerable<Appointment>>(vm.Appointments));
            await _imgRepo.BulkInsertImages(_mapper.Map<IEnumerable<VillaPreviewImages>>(vm.ImageCreateDtos));
        }

    }
}
