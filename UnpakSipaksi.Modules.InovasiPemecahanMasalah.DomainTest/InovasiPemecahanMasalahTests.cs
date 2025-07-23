using FluentAssertions;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.DomainTest
{
    public class InovasiPemecahanMasalahTests
    {
        [Fact]
        public void Create_ShouldReturnInovasiPemecahanMasalahWithCorrectProperties()
        {
            // Arrange
            string Nama = "AA";
            int Skor = 1;

            // Act
            var result = Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah.Create(
                Nama,
                Skor
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(Nama);
            result.Value.Skor.Should().Be(Skor);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_ShouldRaiseInovasiPemecahanMasalahCreatedDomainEvent()
        {
            // Arrange
            string Nama = "AA";
            int Skor = 1;

            // Act
            var result = Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah.Create(
                Nama,
                Skor
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is InovasiPemecahanMasalahCreatedDomainEvent);
        }

        [Theory]
        [InlineData("InovasiPemecahanMasalah.InvalidSkor", "Skor is invalid format", ErrorType.NotFound, "AA", -1)]
        public void Create_ShouldReturnInvalid(string expectedCode, string expectedDescription, ErrorType expectedType, string Nama, int Skor)
        {
            // Arrange
            Error error = expectedCode switch
            {
                "InovasiPemecahanMasalah.InvalidSkor" => InovasiPemecahanMasalahErrors.InvalidSkor(),
                _ => throw new ArgumentException("Unknown error code", nameof(expectedCode))
            };

            // Act
            var result = Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah.Create(
                Nama,
                Skor
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            error.Code.Should().Be(expectedCode);
            error.Description.Should().Be(expectedDescription);
            error.Type.Should().Be(expectedType);
        }
    }
}