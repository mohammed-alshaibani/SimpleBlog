using Microsoft.AspNetCore.SignalR;
using SimpleBlog.Models;

namespace SimpleBlog.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendPostNotification(string title)
        {
            await Clients.All.SendAsync("ReceivePostNotification", title);
        }
    }
}
