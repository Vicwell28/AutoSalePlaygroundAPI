using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands;
using AutoSalePlaygroundAPI.CrossCutting.Helpers;
using FluentValidation;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Validations
{
    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        private readonly List<string> _forbiddenWords = new() { "forbidden1", "forbidden2" };

        public CreateVehicleCommandValidator()
        {
            // Validación para la Marca
            RuleFor(x => x.Marca)
                .NotEmpty().WithMessage("La marca es requerida.")
                .Must(ValidationHelpers.BeAValidString).WithMessage("La marca debe ser una cadena de texto válida.")
                .MaximumLength(50).WithMessage("La marca no debe exceder los 50 caracteres.");

            // Validación para el Precio
            RuleFor(x => x.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.")
                .PrecisionScale(14, 2, false).WithMessage("El precio debe tener un formato numérico válido.");

            // Validación para el Año
            RuleFor(x => x.Año)
                .InclusiveBetween(1500, 3000).WithMessage("El año debe estar entre 1500 y 3000.");

            // Validación para el Modelo
            RuleFor(x => x.Modelo)
                .NotEmpty().WithMessage("El modelo es requerido.")
                .Must(ValidationHelpers.BeAValidString).WithMessage("El modelo debe ser una cadena de texto válida.")
                .MaximumLength(50).WithMessage("El modelo no debe exceder los 50 caracteres.")
                .Must(modelo => ValidationHelpers.NotContainForbiddenWords(modelo, _forbiddenWords))
                .WithMessage("El modelo contiene palabras no permitidas.");
        }
    }
}