using OA.Data;
using System.Collections.Generic;

namespace OA.Service
{
    public interface ITouristPlaceTypeService
    {
        IEnumerable<TouristPlaceType> GetTouristPlaceTypes();
        TouristPlaceType GetTouristPlaceType(int id);
    }
}
