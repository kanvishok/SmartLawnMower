using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using FluentValidation;
using LawnMower.Domain.Commands;
using LawnMower.Domain.Events;
using LawnMower.Infrastructure.Command;
using LawnMower.Infrastructure.Event;
using LawnMower.Infrastructure.Repository;

namespace LawnMower.Domain.CommandHandlers
{
    public class InitiateCommandHandler : ICommandHandler<InitiateCommand>
    {
        private readonly IValidator<InitiateCommand> _validator;

        private readonly IGenericRepository<SmartLawnMower> _smartLawnMowerRepository;

        public InitiateCommandHandler(IGenericRepository<SmartLawnMower> smartLawnMowerRepository, IValidator<InitiateCommand> validator)
        {
            _smartLawnMowerRepository = smartLawnMowerRepository;
            _validator = validator;
        }

        public async Task<IEnumerable<IEvent>> HandleAsync(InitiateCommand command)
        {
            _validator.ValidateCommand(command);
            try
            {
                var smartLawnMover = await _smartLawnMowerRepository.FindBy(x => x.IsActive).FirstOrDefaultAsync();
            
            
            if (smartLawnMover != null)
                SmartLawnMower.Deactivate(smartLawnMover);
            smartLawnMover = SmartLawnMower.Setup(command.SetupParameters);
            _smartLawnMowerRepository.Add(smartLawnMover);
            await _smartLawnMowerRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return new List<IEvent>
            {
                new InitiateCompletedEvent()
            };
        }
    }
}
