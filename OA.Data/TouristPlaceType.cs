using System.Collections.Generic;

namespace OA.Data
{
    public class TouristPlaceType : BaseEntity
    {
        public string TypeName { get; set; }
        public ICollection<TouristPlace> TouristPlaces { get; set; }
    }
}
