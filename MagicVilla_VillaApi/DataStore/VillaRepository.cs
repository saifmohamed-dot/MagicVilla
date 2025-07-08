using MagicVilla_VillaApi.Dto;

namespace MagicVilla_VillaApi.DataStore
{
    public static class VillaRepository
    {
        static List<VillaDto> _villas;
        static int _IdCnt = 0;
        static VillaRepository()
        {
            _villas = new List<VillaDto>()
            {
                new VillaDto()
                {
                    Id = GetNextId(),
                    Details = "Good Villa"
                },
                new VillaDto()
                {
                    Id = GetNextId(),
                    Details = "Bad Villa"
                }
            };
        }
        static int GetNextId()
        {
            return ++_IdCnt;
        }
        public static IEnumerable<VillaDto> GetVillas()
        {
            return _villas;
        }
        public static int AddVilla(VillaDto villa)
        {
            villa.Id = GetNextId();
            _villas.Add(villa);
            return _IdCnt;
        }
        public static void DeleteVilla(VillaDto villa)
        {
            _villas.Remove(villa);
        }
    }
}
