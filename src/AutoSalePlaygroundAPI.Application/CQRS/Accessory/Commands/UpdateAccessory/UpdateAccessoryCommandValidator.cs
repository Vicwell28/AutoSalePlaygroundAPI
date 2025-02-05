using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.UpdateAccessory
{
    public class UpdateAccessoryCommandValidator : AbstractValidator<UpdateAccessoryCommand>
    {
        public UpdateAccessoryCommandValidator()
        {
            RuleFor(x => x.AccessoryId)
                .GreaterThan(0).WithMessage("El Id del accesorio debe ser mayor que 0.");

            RuleFor(x => x.NewName)
                .NotEmpty().WithMessage("El nuevo nombre es requerido.")
                .MaximumLength(100).WithMessage("El nombre no debe exceder 100 caracteres.");
        }
    }
}
