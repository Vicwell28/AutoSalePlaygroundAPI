using AutoSalePlaygroundAPI.Domain.DomainEvent;
using AutoSalePlaygroundAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Linq;


namespace AutoSalePlaygroundAPI.UnitTests.Domain
{
    public class OwnerTests
    {
        [Fact]
        public void UpdateName_Should_UpdateOwnerName_And_RaiseDomainEvent()
        {
            // Arrange: crear un Owner
            var owner = new Owner("Juan", "Perez");

            // Limpia los eventos de dominio pendientes (si usas un contenedor estático)
            DomainEvents.Clear();

            // Act: actualiza el nombre
            owner.UpdateName("Carlos", "Gomez");

            // Assert: se deben actualizar las propiedades
            owner.FirstName.Should().Be("Carlos");
            owner.LastName.Should().Be("Gomez");

            // Assert: se debe haber levantado un evento de dominio de tipo OwnerUpdatedDomainEvent
            var domainEvent = DomainEvents.Events.FirstOrDefault(e => e is OwnerUpdatedDomainEvent);
            domainEvent.Should().NotBeNull("porque al actualizar el nombre se debe generar un evento de dominio");
        }
    }
}