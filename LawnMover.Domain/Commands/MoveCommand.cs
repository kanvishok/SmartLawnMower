using LawnMower.Infrastructure.Command;

namespace LawnMower.Domain.Commands
{
    public class MoveCommand:ICommand
    {
        public MoveCommand(int units)
        {
            Units = units;
        }

        public int  Units { get; private set; }
    }
}
