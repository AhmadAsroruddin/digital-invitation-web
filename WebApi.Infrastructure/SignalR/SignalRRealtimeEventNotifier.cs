using WebApi.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using WebApi.Shared;

namespace WebApi.Infrastructure.SignalR
{
    public class SignalRRealtimeEventNotifier(IHubContext<GuestListHub> hubContext) : IRealtimeEventNotifier
    {
        private readonly IHubContext<GuestListHub> hubContext = hubContext;

        public async Task NotifyToGroupAsync<T>(string group, string eventName, T payload)
        {
            Console.WriteLine($"[SignalR] Notify group: {group} {eventName} {payload}");

            await hubContext.Clients.Group(group).SendAsync(eventName, payload);
        }

        public async Task NotifyToAllAsync<T>(string eventName, T payload)
        {
            await hubContext.Clients.All.SendAsync(eventName, payload);
        }
    }
}
