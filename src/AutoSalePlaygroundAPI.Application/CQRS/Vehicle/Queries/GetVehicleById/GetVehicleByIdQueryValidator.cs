using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehicleById
{
    public class GetVehicleByIdQueryValidator : AbstractValidator<GetVehicleByIdQuery>
    {
        public GetVehicleByIdQueryValidator()
        {
            RuleFor(x => x.VehicleId)
                .GreaterThan(0).WithMessage("El Id del vehículo debe ser mayor que 0.");
        }
    }
}
