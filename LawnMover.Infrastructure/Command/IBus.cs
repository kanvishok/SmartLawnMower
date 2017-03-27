using System.Threading.Tasks;

namespace LawnMower.Infrastructure.Command
{
    public interface IBus
    {
        Task SendAsync<TCommand, TAggregate>(TCommand command) where TCommand : ICommand
            where TAggregate : IAggregateRoot;
    }
}