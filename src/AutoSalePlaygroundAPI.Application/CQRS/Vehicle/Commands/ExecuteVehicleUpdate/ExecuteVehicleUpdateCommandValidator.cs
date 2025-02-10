using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.ExecuteVehicleUpdate
{
    public class ExecuteVehicleUpdateCommandValidator : AbstractValidator<ExecuteVehicleUpdateCommand>
    {
        public ExecuteVehicleUpdateCommandValidator()
        {
            RuleFor(x => x.VehicleId).GreaterThan(0).WithMessage("El VehicleId debe ser mayor que 0.");
            RuleFor(x => x.NewLicensePlate).NotEmpty().WithMessage("El nuevo número de placa es requerido.");
            RuleFor(x => x.FuelType).NotEmpty().WithMessage("El nuevo tipo de combustible es requerido.");
            RuleFor(x => x.EngineDisplacement).GreaterThan(0).WithMessage("La cilindrada debe ser mayor que 0.");
            RuleFor(x => x.Horsepower).GreaterThan(0).WithMessage("La potencia debe ser mayor que 0.");
        }
    }
}