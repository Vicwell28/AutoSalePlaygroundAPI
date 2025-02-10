using AutoSalePlaygroundAPI.Domain.DTOs;
using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.BulkUpdateVehicles
{
    public class BulkUpdateVehiclesCommandValidator : AbstractValidator<BulkUpdateVehiclesCommand>
    {
        public BulkUpdateVehiclesCommandValidator()
        {
            RuleForEach(x => x.VehiclePartialUpdateDtos).SetValidator(new VehiclePartialUpdateDtoValidator());
        }
    }

    public class VehiclePartialUpdateDtoValidator : AbstractValidator<VehiclePartialUpdateDto>
    {
        public VehiclePartialUpdateDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("El Id del vehículo debe ser mayor que 0.");
        }
    }
}
