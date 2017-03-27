using System.Collections.Generic;
using System.Threading.Tasks;
using LawnMower.Infrastructure.Event;

namespace LawnMower.Infrastructure.Command
{
    public interface ICommandHandler<in TCommand> where TCommand:ICommand
    {
        Task<IEnumerable<IEvent>>  HandleAsync(TCommand command);
    }
}