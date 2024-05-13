using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class StaticFileConfiguration : IEntityTypeConfiguration<StaticFile>
{
    public void Configure(EntityTypeBuilder<StaticFile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Path)
            .IsRequired();

    

      
    }
}