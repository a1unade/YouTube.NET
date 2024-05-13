using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description);

        builder.Property(x => x.ViewCount)
            .HasDefaultValue(0);

        builder.Property(x => x.PathInDisk)
            .IsRequired();

        builder.Property(x => x.LikeCount)
            .HasDefaultValue(0);

        builder.Property(x => x.Description)
            .HasDefaultValue(0);

        builder.Property(x => x.ReleaseDate)
            .HasColumnType("date");

        builder.HasOne(x => x.Channel)
            .WithMany(x => x.Videos)
            .HasForeignKey(x => x.ChannelId);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Video)
            .HasForeignKey(x => x.VideoId);
        
        builder.HasOne(x => x.StaticFile)
            .WithOne(x => x.Video)
            .HasForeignKey<Video>(x => x.PreviewImgId);
    }
}