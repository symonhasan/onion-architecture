namespace OA.Data
{
    public class TouristPlace : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Rating { get; set; }
        public string Image { get; set; }
        public int TypeId { get; set; }
        public TouristPlaceType Type { get; set; }
    }
}
