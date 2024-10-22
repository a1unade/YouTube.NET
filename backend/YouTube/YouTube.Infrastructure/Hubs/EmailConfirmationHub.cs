using Microsoft.AspNetCore.SignalR;

namespace YouTube.Infrastructure.Hubs;

public class EmailConfirmationHub: Hub
{
    public async Task SendEmailConfirmationStatus(bool isConfirmed)
    {
        await Clients.All.SendAsync("ReceiveEmailConfirmationStatus", isConfirmed);
    }
}