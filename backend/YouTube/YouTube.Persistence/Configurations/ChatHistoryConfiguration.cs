using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class ChatHistoryConfiguration : IEntityTypeConfiguration<ChatHistory>
{
    public void Configure(EntityTypeBuilder<ChatHistory> builder)
    {
        builder.HasKey(ch => ch.Id);

        builder.Property(ch => ch.StartDate)
            .IsRequired();

        builder.HasOne(ch => ch.User)
            .WithOne(x => x.ChatHistory)
            .HasForeignKey<ChatHistory>(ch => ch.UserId);
    }
}