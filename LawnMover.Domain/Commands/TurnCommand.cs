using LawnMower.Infrastructure.Command;

namespace LawnMower.Domain.Commands
{
    public class TurnCommand : ICommand
    {
        public TurnCommand(string direction)
        {
            Direction = direction;
        }

        public string Direction { get; private set; }
        //public string SmartLawnMowerId { get; private set; }

    }
}