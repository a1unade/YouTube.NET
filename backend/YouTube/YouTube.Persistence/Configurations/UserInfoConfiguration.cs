using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(64);
        
        builder.Property(x => x.Surname)
            .IsRequired()
            .HasMaxLength(64);
        
        builder.Property(x => x.BirthDate);

        builder.Property(x => x.Gender);

        builder.Property(x => x.Country)
            .IsRequired()
            .HasMaxLength(50);
    }
}