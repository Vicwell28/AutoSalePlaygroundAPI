using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandValidator : AbstractValidator<DeleteVehicleCommand>
    {
        public DeleteVehicleCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("El ID del vehículo debe ser mayor a 0");
        }
    }
}
