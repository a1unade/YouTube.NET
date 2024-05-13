using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.CreateDate)
            .HasColumnType("date");

        builder.Property(x => x.SubCount)
            .HasDefaultValue(0);

        builder.Property(x => x.BannerImgId);

        builder.HasOne(x => x.UserInfo)
            .WithOne(x => x.Channel)
            .HasForeignKey<Channel>(x => x.UserId);

        builder.HasMany(x => x.Videos)
            .WithOne(x => x.Channel)
            .HasForeignKey(x => x.ChannelId);

        builder.HasOne(x => x.MainImgFile)
            .WithOne(x => x.Channel)
            .HasForeignKey<Channel>(x => x.MainImgId);
    }
}