using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.Description)
            .HasMaxLength(255);

        builder.Property(x => x.CreateDate)
            .IsRequired();

        builder.Property(x => x.SubCount)
            .HasDefaultValue(0);
        
        builder.Property(x => x.Country)
            .HasMaxLength(50);

        builder.Property(x => x.BannerImgId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Channels)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Videos)
            .WithOne(x => x.Channel)
            .HasForeignKey(x => x.ChannelId);

        builder.HasOne(x => x.MainImgFile)
            .WithOne()
            .HasForeignKey<Channel>(x => x.MainImgId);

        builder.HasOne(x => x.BannerImg)
            .WithOne()
            .HasForeignKey<Channel>(x => x.BannerImgId);
    }
}