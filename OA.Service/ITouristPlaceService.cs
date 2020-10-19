using OA.Data;
using System.Collections.Generic;

namespace OA.Service
{
    public interface ITouristPlaceService
    {
        IEnumerable<TouristPlace> GetTouristPlaces();
        TouristPlace GetTouristPlace(int id);
        void InsertTouristPlace(TouristPlace entity);
        void UpdateTouristPlace(TouristPlace entity);
        void DeleteTouristPlace(TouristPlace entity);
    }
}
