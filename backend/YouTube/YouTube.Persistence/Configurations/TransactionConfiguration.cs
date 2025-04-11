using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Common;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.Price)
            .IsRequired();

        builder.Property(x => x.Operation)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion(
                x => x.ToString(),
                v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v)
                );
        
        builder.Property(x => x.Description)
            .HasMaxLength(255);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.UserId);
    }
}