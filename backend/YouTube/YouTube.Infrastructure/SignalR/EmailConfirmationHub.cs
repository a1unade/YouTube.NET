using Microsoft.AspNetCore.SignalR;

namespace YouTube.Infrastructure.SignalR;

public class EmailConfirmationHub: Hub
{
    public async Task SendEmailConfirmationStatus(bool isConfirmed)
    {
        await Clients.All.SendAsync("ReceiveEmailConfirmationStatus", isConfirmed);
    }
}