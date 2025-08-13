using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.Property(x => x.Expires)
            .IsRequired();

        builder.Property(x => x.Revoked);

        builder.Property(x => x.Created);
        builder.Property(x => x.ReplacedByToken);
        
        builder.Property(x => x.UserId)
            .IsRequired();
        
        builder.HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens) 
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade); 
        
    }
}