using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.DeleteAccessory
{
    public class DeleteAccessoryCommandValidator : AbstractValidator<DeleteAccessoryCommand>
    {
        public DeleteAccessoryCommandValidator()
        {
            RuleFor(x => x.AccessoryId)
                .GreaterThan(0).WithMessage("El Id del accesorio debe ser mayor que 0.");
        }
    }
}