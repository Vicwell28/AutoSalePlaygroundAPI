using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.ChangeVehicleOwner
{
    public class ChangeVehicleOwnerCommandValidator : AbstractValidator<ChangeVehicleOwnerCommand>
    {
        public ChangeVehicleOwnerCommandValidator()
        {
            RuleFor(x => x.VehicleId)
                .GreaterThan(0).WithMessage("El Id del vehículo debe ser mayor que 0.");
            RuleFor(x => x.NewOwnerId)
                .GreaterThan(0).WithMessage("El Id del nuevo propietario debe ser mayor que 0.");
        }
    }
}
