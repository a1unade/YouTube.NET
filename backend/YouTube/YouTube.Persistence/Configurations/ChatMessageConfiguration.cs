using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Timestamp)
            .IsRequired();

        builder.Property(x => x.Message);

        builder.Property(x => x.IsRead);

        builder.HasOne(x => x.ChatHistory)
            .WithMany(x => x.ChatMessages)
            .HasForeignKey(x => x.ChatHistoryId);

        builder.HasOne(x => x.File)
            .WithOne()
            .HasForeignKey<ChatMessage>(x => x.FileId);
    }
}