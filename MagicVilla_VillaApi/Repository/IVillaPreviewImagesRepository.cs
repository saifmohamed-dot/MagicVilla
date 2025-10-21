using MagicVilla_VillaApi.Models;

namespace MagicVilla_VillaApi.Repository
{
    public interface IVillaPreviewImagesRepository : IRepository<VillaPreviewImages>
    {
        //Task<IEnumerable<VillaPreviewImages>> GetPreviewImagesByVillaId(int villaId);
        Task BulkInsertImages(IEnumerable<VillaPreviewImages> images);
    }
}
