using MassTransit;
using YouTube.Application.Common.Requests.Video;

namespace YouTube.BusAPI.Consumers;

public class IncrementViewConsumer : IConsumer<SendIncrementViewMessage>
{
    public Task Consume(ConsumeContext<SendIncrementViewMessage> context)
    {
        Console.WriteLine("Consumed message from IncrementViewConsumer");
        Console.WriteLine(context.Message.VideoId);  
        Console.WriteLine(context.Message.Id);
        Console.WriteLine(context.Message.ViewCount);

        return Task.CompletedTask;
    }
}