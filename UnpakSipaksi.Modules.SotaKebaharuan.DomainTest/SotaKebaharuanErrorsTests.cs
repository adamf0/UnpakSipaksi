using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan;

namespace UnpakSipaksi.Modules.SotaKebaharuan.DomainTest
{
    public class SotaKebaharuanErrorsTests
    {
        [Theory]
        [InlineData("SotaKebaharuan.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("SotaKebaharuan.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("SotaKebaharuan.InvalidValueSkor", "Invalid value 'skor'", ErrorType.NotFound)]
        [InlineData("SotaKebaharuan.NotSameValue", "not the same value in data 'skor'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "SotaKebaharuan.EmptyData" => SotaKebaharuanErrors.EmptyData(),
                "SotaKebaharuan.UnknownKategoriSkema" => SotaKebaharuanErrors.UnknownKategoriSkema(),
                "SotaKebaharuan.InvalidValueSkor" => SotaKebaharuanErrors.InvalidValueSkor(),
                "SotaKebaharuan.NotSameValue" => SotaKebaharuanErrors.NotSameValue(),
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
            var error = SotaKebaharuanErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("SotaKebaharuan.NotFound");
            error.Description.Should().Be($"Sota kebaharuan with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
