using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.CreateVehicle
{
    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
        {
            RuleFor(x => x.LicensePlateNumber)
                .NotEmpty().WithMessage("El número de placa es requerido.")
                .MaximumLength(20).WithMessage("El número de placa no debe exceder los 20 caracteres.");

            RuleFor(x => x.OwnerId)
                .GreaterThan(0).WithMessage("El Id del propietario debe ser mayor que 0.");

            RuleFor(x => x.FuelType)
                .NotEmpty().WithMessage("El tipo de combustible es requerido.")
                .MaximumLength(50).WithMessage("El tipo de combustible no debe exceder los 50 caracteres.");

            RuleFor(x => x.EngineDisplacement)
                .GreaterThan(0).WithMessage("La cilindrada debe ser mayor que 0.");

            RuleFor(x => x.Horsepower)
                .GreaterThan(0).WithMessage("La potencia debe ser mayor que 0.");
        }
    }
}
