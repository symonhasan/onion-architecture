using OA.Data;
using OA.Repository;
using System.Collections.Generic;

namespace OA.Service
{
    public class TouristPlaceTypeService : ITouristPlaceTypeService
    {
        private IRepository<TouristPlaceType> touristPlaceTypeRepository;

        public TouristPlaceTypeService(IRepository<TouristPlaceType> _touristPlaceTypeRepository)
        {
            touristPlaceTypeRepository = _touristPlaceTypeRepository;
        }

        public IEnumerable<TouristPlaceType> GetTouristPlaceTypes()
        {
            return touristPlaceTypeRepository.GetAll();
        }

        public TouristPlaceType GetTouristPlaceType(int id)
        {
            return touristPlaceTypeRepository.Get(id);
        }
    }
}
