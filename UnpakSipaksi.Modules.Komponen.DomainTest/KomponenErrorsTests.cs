using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Komponen.Domain.Komponen;
using Xunit;

namespace UnpakSipaksi.Modules.Komponen.DomainTest
{
    public class KomponenErrorsTests
    {
        [Theory]
        [InlineData("Komponen.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("Komponen.NamaEmpty", "Nama can't be empty", ErrorType.NotFound)]
        [InlineData("Komponen.InvalidMaxBiaya", "MaxBiaya is invalid format", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "Komponen.EmptyData" => KomponenErrors.EmptyData(),
                "Komponen.NamaEmpty" => KomponenErrors.NamaEmpty(),
                "Komponen.InvalidMaxBiaya" => KomponenErrors.InvalidMaxBiaya(),
                _ => throw new ArgumentException("Unknown error code", nameof(expectedCode))
            };

            // Assert
            error.Code.Should().Be(expectedCode);
            error.Description.Should().Be(expectedDescription);
            error.Type.Should().Be(expectedType);
        }

        [Fact]
        public void NotFound_ShouldReturnCorrectError()
        {
            // Arrange
            var komponenId = Guid.NewGuid();

            // Act
            var error = KomponenErrors.NotFound(komponenId);

            // Assert
            error.Code.Should().Be("Komponen.NotFound");
            error.Description.Should().Be($"Komponen with the identifier {komponenId} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public void InvalidMaxBiaya_ShouldReturnCorrectError()
        {
            // Act
            var error = KomponenErrors.InvalidMaxBiaya();

            // Assert
            error.Code.Should().Be("Komponen.InvalidMaxBiaya");
            error.Description.Should().Be("MaxBiaya is invalid format");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
