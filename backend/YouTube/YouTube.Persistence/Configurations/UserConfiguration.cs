using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.HasOne(u => u.UserInfo)
                .WithOne(x => x.User)
                .HasForeignKey<UserInfo>(x => x.UserId)
                .HasPrincipalKey<User>(x => x.Id);
        }
    }
}