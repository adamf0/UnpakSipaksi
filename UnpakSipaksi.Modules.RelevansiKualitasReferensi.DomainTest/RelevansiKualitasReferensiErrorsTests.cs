using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.DomainTest
{
    public class RelevansiKualitasReferensiErrorsTests
    {
        [Theory]
        [InlineData("RelevansiKualitasReferensi.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("RelevansiKualitasReferensi.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("RelevansiKualitasReferensi.InvalidValueSkor", "Invalid value 'skor'", ErrorType.NotFound)]
        [InlineData("RelevansiKualitasReferensi.NotSameValue", "not the same value in data 'skor'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "RelevansiKualitasReferensi.EmptyData" => RelevansiKualitasReferensiErrors.EmptyData(),
                "RelevansiKualitasReferensi.UnknownKategoriSkema" => RelevansiKualitasReferensiErrors.UnknownKategoriSkema(),
                "RelevansiKualitasReferensi.InvalidValueSkor" => RelevansiKualitasReferensiErrors.InvalidValueSkor(),
                "RelevansiKualitasReferensi.NotSameValue" => RelevansiKualitasReferensiErrors.NotSameValue(),
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
            var error = RelevansiKualitasReferensiErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("RelevansiKualitasReferensi.NotFound");
            error.Description.Should().Be($"Relevansi kualitas referensi with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
