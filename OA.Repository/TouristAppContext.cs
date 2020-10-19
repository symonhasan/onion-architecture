using Microsoft.EntityFrameworkCore;
using OA.Data;

namespace OA.Repository
{
    public class TouristAppContext : DbContext
    {
        public TouristAppContext(DbContextOptions<TouristAppContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new TouristPlaceMap(modelBuilder.Entity<TouristPlace>());
            new TouristPlaceTypeMap(modelBuilder.Entity<TouristPlaceType>());
            new UserMap(modelBuilder.Entity<User>());
        }
    }
}
