using System.Threading.Tasks;

namespace WebApi.Application.Interfaces
{
    public interface IRealtimeEventNotifier
    {
        Task NotifyToGroupAsync<T>(string group, string eventName, T payload);
        Task NotifyToAllAsync<T>(string eventName, T payload);
    }
}
