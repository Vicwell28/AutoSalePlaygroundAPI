using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehiclesDynamic
{
    public class GetVehiclesDynamicQueryValidator : FluentValidation.AbstractValidator<GetVehiclesDynamicQuery>
    {
        public GetVehiclesDynamicQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("El número de página debe ser mayor que 0.");
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("El tamaño de página debe ser mayor que 0.");
        }
    }
}
