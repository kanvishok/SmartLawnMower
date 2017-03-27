using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using FluentValidation;
using LawnMower.Domain.Commands;
using LawnMower.Domain.Entity;
using LawnMower.Domain.Events;
using LawnMower.Infrastructure.Command;
using LawnMower.Infrastructure.Event;
using LawnMower.Infrastructure.Repository;

namespace LawnMower.Domain.CommandHandlers
{
    public class MoveCommandHandler : ICommandHandler<MoveCommand>
    {
        private readonly IValidator<MoveCommand> _validator;

        private readonly IGenericRepository<SmartLawnMower> _smartLawnMowerRepository;
        private readonly IGenericRepository<Log> _logRepository;
        public MoveCommandHandler(IValidator<MoveCommand> validator, IGenericRepository<Log> logRepository, IGenericRepository<SmartLawnMower> smartLawnMowerRepository)
        {
            _validator = validator;
            _logRepository = logRepository;
            _smartLawnMowerRepository = smartLawnMowerRepository;
        }
        public async Task<IEnumerable<IEvent>> HandleAsync(MoveCommand command)
        {
            var smartLawnMover = await _smartLawnMowerRepository.FindBy(x => x.IsActive).FirstOrDefaultAsync();

            _validator.ValidateCommand(command);
            _logRepository.Add(new Log
            {
                LogMessage = $"{DateTime.Now.ToLongTimeString()} - Start move from ({smartLawnMover.Location.X},{smartLawnMover.Location.Y})"
            });
            await _logRepository.SaveChangesAsync();
            SmartLawnMower.Move(smartLawnMover, command.Units);
            _smartLawnMowerRepository.Edit(smartLawnMover);
            await _smartLawnMowerRepository.SaveChangesAsync();
            _logRepository.Add(new Log
            {
                LogMessage = $"{DateTime.Now.ToLongTimeString()} - Start move from ({smartLawnMover.Location.X},{smartLawnMover.Location.Y})"
            });
            await _logRepository.SaveChangesAsync();

            return new List<IEvent>
            {
                new MoveCompletedEvent()
            };
        }
    }
}
