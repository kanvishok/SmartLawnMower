using System;
using System.Threading.Tasks;
using LawnMower.Domain.Events;
using LawnMower.Infrastructure.Event;

namespace LawnMower.Domain.EventHandlers
{
    public class TurnCompletedEventHandler:IEventHandler<TurnCompletedEvent>
    {
        public Task HandleAsync(TurnCompletedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
