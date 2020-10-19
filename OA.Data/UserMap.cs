using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OA.Data
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.HasKey(user => user.Id);
            entityBuilder.Property(user => user.FirstName).IsRequired();
            entityBuilder.Property(user => user.LastName).IsRequired();
            entityBuilder.Property(user => user.Email).IsRequired();
            entityBuilder.Property(user => user.Password).IsRequired();
        }
    }
}
