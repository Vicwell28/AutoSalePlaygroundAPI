using AutoSalePlaygroundAPI.CrossCutting.Helpers;
using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        private readonly List<string> _forbiddenWords = new() { "forbidden1", "forbidden2" };

        public UpdateVehicleCommandValidator()
        {
            // Validación para el ID
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID debe ser mayor a 0.");
        }
    }
}
