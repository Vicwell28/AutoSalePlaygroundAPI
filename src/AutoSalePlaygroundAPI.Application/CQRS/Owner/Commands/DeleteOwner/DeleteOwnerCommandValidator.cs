using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.DeleteOwner
{
    public class DeleteOwnerCommandValidator : AbstractValidator<DeleteOwnerCommand>
    {
        public DeleteOwnerCommandValidator()
        {
            RuleFor(x => x.OwnerId)
                .GreaterThan(0).WithMessage("El Id del propietario debe ser mayor que 0.");
        }
    }
}
