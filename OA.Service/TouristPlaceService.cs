using OA.Data;
using OA.Repository;
using System.Collections.Generic;

namespace OA.Service
{
    public class TouristPlaceService : ITouristPlaceService
    {
        private IRepository<TouristPlace> touristPlaceRepository;

        public TouristPlaceService(IRepository<TouristPlace> _touristPlaceRepository)
        {
            touristPlaceRepository = _touristPlaceRepository;
        }

        public IEnumerable<TouristPlace> GetTouristPlaces()
        {
            return touristPlaceRepository.GetAll();
        }

        public TouristPlace GetTouristPlace(int id)
        {
            return touristPlaceRepository.Get(id);
        }

        public void InsertTouristPlace(TouristPlace entity)
        {
            touristPlaceRepository.Insert(entity);
        }

        public void UpdateTouristPlace(TouristPlace entity)
        {
            touristPlaceRepository.Update(entity);
        }

        public void DeleteTouristPlace(TouristPlace entity)
        {
            touristPlaceRepository.Delete(entity);
        }
    }
}
