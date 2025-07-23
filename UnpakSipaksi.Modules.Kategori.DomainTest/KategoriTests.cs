using FluentAssertions;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.Domain;
using UnpakSipaksi.Modules.Kategori.Domain.Kategori;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UnpakSipaksi.Modules.Kategori.DomainTest
{
    public class KategoriTests
    {
        [Fact]
        public void Create_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.Kategori.Kategori.Create(
                nama
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(nama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_ShouldRaiseKategoriCreatedDomainEvent()
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.Kategori.Kategori.Create(
                nama
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is KategoriCreatedDomainEvent);
        }
    }
}