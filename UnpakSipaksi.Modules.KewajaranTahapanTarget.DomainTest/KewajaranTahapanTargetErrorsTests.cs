using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.DomainTest
{
    public class KewajaranTahapanTargetErrorsTests
    {
        [Theory]
        [InlineData("KewajaranTahapanTarget.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KewajaranTahapanTarget.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KewajaranTahapanTarget.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("KewajaranTahapanTarget.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KewajaranTahapanTarget.EmptyData" => KewajaranTahapanTargetErrors.EmptyData(),
                "KewajaranTahapanTarget.UnknownKategoriSkema" => KewajaranTahapanTargetErrors.UnknownKategoriSkema(),
                "KewajaranTahapanTarget.InvalidValueNilai" => KewajaranTahapanTargetErrors.InvalidValueNilai(),
                "KewajaranTahapanTarget.NotSameValue" => KewajaranTahapanTargetErrors.NotSameValue(),
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
            var id = Guid.NewGuid();

            // Act
            var error = KewajaranTahapanTargetErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KewajaranTahapanTarget.NotFound");
            error.Description.Should().Be($"Kewajaran tahapan target with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
