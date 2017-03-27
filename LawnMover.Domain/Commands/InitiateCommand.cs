using LawnMower.Infrastructure.Command;
using LawnMower.Shared.Model;

namespace LawnMower.Domain.Commands
{
    public class InitiateCommand : ICommand
    {
        public InitiateCommand(LawnMowerSetupParams setupParameters)
        {
            SetupParameters = setupParameters;
        }
        public LawnMowerSetupParams SetupParameters { get; private set; }
    }
}
