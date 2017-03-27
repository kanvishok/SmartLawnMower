using System.Threading.Tasks;

namespace LawnMower.Infrastructure.Event
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}