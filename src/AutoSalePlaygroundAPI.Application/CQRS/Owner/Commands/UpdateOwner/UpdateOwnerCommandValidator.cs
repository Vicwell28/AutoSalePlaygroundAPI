using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.UpdateOwner
{
    public class UpdateOwnerCommandValidator : AbstractValidator<UpdateOwnerCommand>
    {
        public UpdateOwnerCommandValidator()
        {
            RuleFor(x => x.OwnerId)
                .GreaterThan(0).WithMessage("El Id del propietario debe ser mayor que 0.");

            RuleFor(x => x.NewFirstName)
                .NotEmpty().WithMessage("El nuevo nombre es requerido.")
                .MaximumLength(50).WithMessage("El nombre no debe exceder 50 caracteres.");

            RuleFor(x => x.NewLastName)
                .NotEmpty().WithMessage("El nuevo apellido es requerido.")
                .MaximumLength(50).WithMessage("El apellido no debe exceder 50 caracteres.");
        }
    }
}
