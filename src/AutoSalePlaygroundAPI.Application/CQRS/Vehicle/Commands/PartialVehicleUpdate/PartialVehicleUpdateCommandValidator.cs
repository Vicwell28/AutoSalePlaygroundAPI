using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.PartialVehicleUpdate
{
    public class PartialVehicleUpdateCommandValidator : AbstractValidator<PartialVehicleUpdateCommand>
    {
        public PartialVehicleUpdateCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("El Id del vehículo debe ser mayor que 0.");
        }
    }
}