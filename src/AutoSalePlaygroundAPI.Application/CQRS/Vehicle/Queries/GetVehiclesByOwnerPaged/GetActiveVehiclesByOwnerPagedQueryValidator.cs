using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetActiveVehiclesByOwnerPaged
{
    public class GetActiveVehiclesByOwnerPagedQueryValidator : AbstractValidator<GetActiveVehiclesByOwnerPagedQuery>
    {
        public GetActiveVehiclesByOwnerPagedQueryValidator()
        {
            RuleFor(x => x.OwnerId)
                .GreaterThan(0).WithMessage("El Id del propietario debe ser mayor que 0.");
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("El número de página debe ser mayor que 0.");
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("El tamaño de página debe ser mayor que 0.");
        }
    }
}
