using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.CreateAccessory
{
    public class CreateAccessoryCommandValidator : AbstractValidator<CreateAccessoryCommand>
    {
        public CreateAccessoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del accesorio es requerido.")
                .MaximumLength(100).WithMessage("El nombre no debe exceder 100 caracteres.");
        }
    }
}
