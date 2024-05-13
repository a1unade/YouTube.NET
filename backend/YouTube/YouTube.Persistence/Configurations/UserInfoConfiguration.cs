using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.Property(x => x.BirthDate)
            .HasColumnType("date");
        
        builder.HasOne(u => u.Avatar)
            .WithOne()
            .HasForeignKey<UserInfo>(u => u.AvatarId);
    }
}