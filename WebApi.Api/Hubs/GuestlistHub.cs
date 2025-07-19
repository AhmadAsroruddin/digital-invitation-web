using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace API.Hubs
{
    public class GuestListHub : Hub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task TriggerManual()
        {
            await Clients.Group("event_3").SendAsync("RSVPUpdated", true);
        }
    }
}
