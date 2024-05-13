using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CommentText)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.LikeCount)
            .HasDefaultValue(0);
        
        builder.Property(x => x.DisLikeCount)
            .HasDefaultValue(0);

        builder.Property(x => x.PostDate)
            .HasColumnType("date");

        builder.HasOne(x => x.Video)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.VideoId);

        builder.HasOne(x => x.UserInfo)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.UserId);

    }
}