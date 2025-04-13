using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Payment.Data.Entities;

namespace YouTube.Payment.Data.Configuration;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserIdPostgres).IsRequired();

        builder.Property(x => x.Balance).IsRequired();

        builder.Property(x => x.UserName).IsRequired();
    }
}