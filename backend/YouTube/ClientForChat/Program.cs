using Grpc.Core;
using Grpc.Net.Client;
using YouTube.Proto;

var userId = "8e666a70-bd9a-4361-8af4-74a3f04051f3";
string chatId = ""; // оставим пустым — сервер создаст

using var channel = GrpcChannel.ForAddress("http://localhost:8081");
var client = new ChatService.ChatServiceClient(channel);

// Получаем chatId
var joinResponse = await client.JoinChatAsync(new JoinChatRequest { UserId = userId, ChatId = chatId });
Console.WriteLine(joinResponse.ChatId);
chatId = joinResponse.ChatId;

// Запускаем получение сообщений
var stream = client.MessageStream(new JoinChatRequest { UserId = userId, ChatId = chatId });

_ = Task.Run(async () =>
{
    await foreach (var msg in stream.ResponseStream.ReadAllAsync())
    {
        Console.WriteLine($"{msg.Time} | {msg.UserId}: {msg.Message}");
    }
});

// Отправка сообщений
while (true)
{
    var input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input)) continue;

    await client.SendMessageAsync(new SendMessageRequest
    {
        ChatId = chatId,
        UserId = userId,
        Message = input
    });
}