using MagicVilla_VillaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Repository
{
    public class VillaPreviewImagesRepository : Repository<VillaPreviewImages>, IVillaPreviewImagesRepository
    {
        readonly DbContext _dbContext;
        public VillaPreviewImagesRepository(DbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task BulkInsertImages(IEnumerable<VillaPreviewImages> images)
        {
            _dbContext.Set<VillaPreviewImages>().AddRange(images);
            await _dbContext.SaveChangesAsync();
        }

        //public async Task<IEnumerable<VillaPreviewImages>> GetPreviewImagesByVillaId(int villaId)
        //{
        //    return await _dbContext.Set<VillaPreviewImages>().Where(vpi => vpi.VillaId == villaId).ToListAsync();
        //}
    }
}
