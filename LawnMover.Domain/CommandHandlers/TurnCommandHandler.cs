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
using LawnMower.Shared.Helper;
using LawnMower.Shared.Model;

namespace LawnMower.Domain.CommandHandlers
{
    public class TurnCommandHandler : ICommandHandler<TurnCommand>
    {
        private readonly IValidator<TurnCommand> _validator;

        private readonly IGenericRepository<SmartLawnMower> _smartLawnMowerRepository;
        private readonly IDirectionHelper _directionHelper;
        private readonly IGenericRepository<Log> _logRepository;
        public TurnCommandHandler(IValidator<TurnCommand> validator, IGenericRepository<SmartLawnMower> smartLawnMowerRepository,
            IGenericRepository<Log> logRepository, IDirectionHelper directionHelper)
        {
            _validator = validator;
            _smartLawnMowerRepository = smartLawnMowerRepository;
            _logRepository = logRepository;
            _directionHelper = directionHelper;
        }
        public async Task<IEnumerable<IEvent>> HandleAsync(TurnCommand command)
        {
            var smartLawnMover = await _smartLawnMowerRepository.FindBy(x => x.IsActive).FirstOrDefaultAsync();

            _validator.ValidateCommand(command);

            var side = _directionHelper.GetSideFromDirection(smartLawnMover.Direction,
                (Direction)Enum.Parse(typeof(Direction), command.Direction));

            _logRepository.Add(new Log
            {
                LogMessage = $"{DateTime.Now.ToLongTimeString()} - Start Turn {side}"
            });
            await _logRepository.SaveChangesAsync();


            smartLawnMover = SmartLawnMower.Turn(command.Direction, smartLawnMover);

            _smartLawnMowerRepository.Edit(smartLawnMover);

            await _smartLawnMowerRepository.SaveChangesAsync();

            _logRepository.Add(new Log
            {
                LogMessage = $"{DateTime.Now.ToLongTimeString()} - End Turn {side}"
            });

            await _logRepository.SaveChangesAsync();

            return new List<IEvent>
            {
                new TurnCompletedEvent()
            };
        }
    }
}