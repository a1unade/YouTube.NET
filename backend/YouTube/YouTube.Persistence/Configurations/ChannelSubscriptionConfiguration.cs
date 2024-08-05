using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Configurations;

public class ChannelSubscriptionConfiguration : IEntityTypeConfiguration<ChannelSubscription>
{
    public void Configure(EntityTypeBuilder<ChannelSubscription> builder)
    {
        builder.HasKey(x => new {x.SubscribedToChannelId, x.SubscriberChannelId});

        builder.HasOne(s => s.Subscriber)
            .WithMany(u => u.Subscriptions)
            .HasForeignKey(s => s.SubscriberChannelId);

        builder.HasOne(x => x.SubscribedTo)
            .WithMany(x => x.Subscribers)
            .HasForeignKey(x => x.SubscribedToChannelId);
    }
}