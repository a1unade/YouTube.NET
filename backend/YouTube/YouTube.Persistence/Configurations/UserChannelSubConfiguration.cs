using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class UserChannelSubConfiguration : IEntityTypeConfiguration<UserChannelSub>
{
    public void Configure(EntityTypeBuilder<UserChannelSub> builder)
    {
        builder.HasKey(x => new {x.UserId, x.ChannelId});
        
        builder.HasOne(s => s.UserInfo)
            .WithMany(u => u.ChannelSubs)
            .HasForeignKey(s => s.UserId);

        builder.HasOne(x => x.Channel)
            .WithMany(x => x.UserChannelSubs)
            .HasForeignKey(x => x.ChannelId);  

    }
}