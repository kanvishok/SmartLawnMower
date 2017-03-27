using FluentValidation;
using LawnMower.Domain.Commands;

namespace LawnMower.Domain.Validators
{
    public class InitiateCommandValidator : AbstractValidator<InitiateCommand>
    {
        public InitiateCommandValidator()
        {
            
        }
    }
}
