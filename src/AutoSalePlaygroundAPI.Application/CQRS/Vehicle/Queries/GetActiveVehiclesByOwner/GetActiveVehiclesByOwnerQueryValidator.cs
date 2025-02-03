using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetActiveVehiclesByOwner
{
    public class GetActiveVehiclesByOwnerQueryValidator : AbstractValidator<GetActiveVehiclesByOwnerQuery>
    {
        public GetActiveVehiclesByOwnerQueryValidator()
        {
            RuleFor(x => x.OwnerId)
                .GreaterThan(0).WithMessage("El Id del propietario debe ser mayor que 0.");
        }
    }
}
