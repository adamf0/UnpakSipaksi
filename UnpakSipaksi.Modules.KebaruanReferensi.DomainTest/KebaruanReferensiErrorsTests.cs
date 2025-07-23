using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi;

namespace UnpakSipaksi.Modules.KebaruanReferensi.DomainTest
{
    public class KebaruanReferensiErrorsTests
    {
        [Theory]
        [InlineData("KebaruanReferensi.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KebaruanReferensi.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KebaruanReferensi.InvalidValueSkor", "Invalid value 'skor'", ErrorType.NotFound)]
        [InlineData("KebaruanReferensi.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KebaruanReferensi.EmptyData" => KebaruanReferensiErrors.EmptyData(),
                "KebaruanReferensi.UnknownKategoriSkema" => KebaruanReferensiErrors.UnknownKategoriSkema(),
                "KebaruanReferensi.InvalidValueSkor" => KebaruanReferensiErrors.InvalidValueSkor(),
                "KebaruanReferensi.NotSameValue" => KebaruanReferensiErrors.NotSameValue(),
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
            var error = KebaruanReferensiErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KebaruanReferensi.NotFound");
            error.Description.Should().Be($"Kebaruan referensi with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
