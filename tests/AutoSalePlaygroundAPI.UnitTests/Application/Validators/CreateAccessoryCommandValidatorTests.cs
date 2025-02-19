using AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.CreateAccessory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace AutoSalePlaygroundAPI.UnitTests.Application.Validators
{
    public class CreateAccessoryCommandValidatorTests
    {
        private readonly CreateAccessoryCommandValidator _validator;

        public CreateAccessoryCommandValidatorTests()
        {
            _validator = new CreateAccessoryCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            // Arrange
            var command = new CreateAccessoryCommand(string.Empty);

            var result = _validator.TestValidate(command);

            // Act & Assert
            result.ShouldHaveValidationErrorFor(person => person.Name);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Name_Is_Valid()
        {
            // Arrange
            var command = new CreateAccessoryCommand("Accesorio Valido");

            var result = _validator.TestValidate(command);

            // Act & Assert
            result.ShouldNotHaveValidationErrorFor(person => person.Name);
        }
    }
}