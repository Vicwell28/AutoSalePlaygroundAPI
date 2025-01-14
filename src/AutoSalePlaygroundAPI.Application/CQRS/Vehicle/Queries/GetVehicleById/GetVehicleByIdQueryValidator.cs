using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehicleById
{
    public class GetVehicleByIdQueryValidator : AbstractValidator<GetVehicleByIdQuery>
    {
        public GetVehicleByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .GreaterThan(0).WithMessage("El ID debe ser mayor a 0.");
        }
    }
}