using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.DTOs.Chat;
using YouTube.Application.DTOs.Video;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;
using File = YouTube.Domain.Entities.File;

namespace YouTube.Infrastructure.Consumers;

public class ChatConsumer : IConsumer<MessageInfo>
{
    private readonly IDbContext _context;
    private readonly IS3Service _s3Service;

    public ChatConsumer(IDbContext context, IS3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }

    public async Task Consume(ConsumeContext<MessageInfo> context)
    {
        if (!context.Message.Message.IsNullOrEmpty())
            Console.WriteLine($"Получено сообщение: {context.Message.Message}");

        if (context.Message.FileContent is not null)
        {
            Console.WriteLine($"Получен файл {context.Message.FileContent.FileName}");
            Console.WriteLine($"Тип {context.Message.FileContent.ContentType}");
            Console.WriteLine($"Размер {context.Message.FileContent.Lenght}");
            Console.WriteLine($"Контент {context.Message.FileContent.Bytes}");
        }

        var chatHistory = await _context.ChatHistories.FirstOrDefaultAsync(x => x.UserId == context.Message.UserId);

        if (chatHistory is null)
        {
            chatHistory = new ChatHistory
            {
                StartDate = DateTime.UtcNow,
                UserId = context.Message.UserId,
                ChatMessages = new List<ChatMessage>()
            };

            await _context.ChatHistories.AddAsync(chatHistory);
        }

        var message = new ChatMessage
        {
            Message = context.Message.Message,
            Timestamp = DateTime.UtcNow,
            UserId = context.Message.UserId,
            ChatHistoryId = chatHistory.Id,
            ChatHistory = chatHistory
        };

        if (context.Message.FileContent is not null)
        {
            var path = await _s3Service.UploadAsync(new FileContent
            {
                Content = new MemoryStream(context.Message.FileContent.Bytes!),
                FileName = context.Message.FileContent.FileName,
                ContentType = context.Message.FileContent.ContentType,
                Lenght = context.Message.FileContent.Lenght,
                Bucket = context.Message.FileContent.Bucket 
            }, default);

            var file = new File
            {
                Size = context.Message.FileContent.Lenght,
                ContentType = context.Message.FileContent.ContentType,
                Path = path,
                FileName = context.Message.FileContent.FileName,
                BucketName = context.Message.FileContent.Bucket
            };

            message.File = file;
        }

        await _context.ChatMessages.AddAsync(message);
        await _context.SaveChangesAsync();
    }
}