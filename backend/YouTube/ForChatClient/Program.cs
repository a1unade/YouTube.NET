using Grpc.Core;
using Grpc.Net.Client;
using YouTube.Proto;

Console.Write("Введите имя пользователя: ");
var userId = "4f368cee-e0f2-400c-a1c9-0d497b0912db";
var chatId = "b8acecd0-c8af-4472-80a0-a8670f17c3a9"; // оставим пустым — сервер создаст

using var channel = GrpcChannel.ForAddress("http://localhost:8081");
var client = new ChatService.ChatServiceClient(channel);

// Получаем chatId
var joinResponse = await client.JoinChatAsync(new JoinChatRequest { UserId = userId, ChatId = chatId });
chatId = joinResponse.ChatId;
Console.WriteLine(chatId);

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