using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OA.Data
{
    public class TouristPlaceMap
    {
        public TouristPlaceMap(EntityTypeBuilder<TouristPlace> entityBuilder)
        {
            entityBuilder.HasKey(place => place.Id);
            entityBuilder.Property(place => place.Name).IsRequired();
            entityBuilder.Property(place => place.Address).IsRequired();
            entityBuilder.Property(place => place.Rating).IsRequired();
            entityBuilder.Property(place => place.TypeId).IsRequired();
            entityBuilder.Property(place => place.Image);
            entityBuilder.HasOne(place => place.Type).WithMany(placeType => placeType.TouristPlaces)
                .HasForeignKey(place => place.TypeId);
        }
    }
}
