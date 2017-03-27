using FluentValidation;
using LawnMower.Domain.Commands;

namespace LawnMower.Domain.Validators
{
    public class MoveCommandValidator : AbstractValidator<MoveCommand>
    {
        public MoveCommandValidator()
        {
            
        }
    }
}
