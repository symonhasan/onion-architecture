using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OA.Data
{
    public class TouristPlaceTypeMap
    {
        public TouristPlaceTypeMap(EntityTypeBuilder<TouristPlaceType> entityBuilder)
        {
            entityBuilder.HasKey(placeType => placeType.Id);
            entityBuilder.Property(placeType => placeType.TypeName).IsRequired();
        }
    }
}
