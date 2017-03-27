using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LawnMower.Domain.Commands;
using LawnMower.Infrastructure.Repository;
using LawnMower.Shared.Helper;
using LawnMower.Shared.Model;

namespace LawnMower.Domain.Validators
{
    public class TurnCommandValidator : AbstractValidator<TurnCommand>
    {
        private readonly IDirectionHelper _directionHelper;
        private readonly IGenericRepository<SmartLawnMower> _smartLawnMowerRepository;
        public TurnCommandValidator(IDirectionHelper directionHelper, IGenericRepository<SmartLawnMower> smartLawnMowerRepository)
        {
            _directionHelper = directionHelper;
            _smartLawnMowerRepository = smartLawnMowerRepository;
            RuleFor(c => c)
                .MustAsync(MoveLeftOrRight)
                .WithMessage("Invalid direction to move");
        }

        private async Task<bool> MoveLeftOrRight(TurnCommand command, CancellationToken cancellationToken)
        {
            var smartLawnMover = await _smartLawnMowerRepository.FindBy(x => x.IsActive).FirstOrDefaultAsync(cancellationToken);
            var side = _directionHelper.GetSideFromDirection(smartLawnMover.Direction,
              (Direction)Enum.Parse(typeof(Direction), command.Direction));
            return side != "Invalid";

        }
    }
}