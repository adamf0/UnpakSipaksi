using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Referensi.Domain.Referensi;

namespace UnpakSipaksi.Modules.Referensi.DomainTest
{
    public class ReferensiErrorsTests
    {
        [Theory]
        [InlineData("Referensi.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("Referensi.NotFoundKebaruanReferensiId", "data kebaruanReferensiId is not found", ErrorType.NotFound)]
        [InlineData("Referensi.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("Referensi.NotFoundRelevansiKualitasReferensiId", "data relevansiKualitasReferensiId is not found", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "Referensi.EmptyData" => ReferensiErrors.EmptyData(),
                "Referensi.NotFoundKebaruanReferensiId" => ReferensiErrors.NotFoundKebaruanReferensiId(),
                "Referensi.InvalidValueNilai" => ReferensiErrors.InvalidValueNilai(),
                "Referensi.NotFoundRelevansiKualitasReferensiId" => ReferensiErrors.NotFoundRelevansiKualitasReferensiId(),
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
            var error = ReferensiErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("Referensi.NotFound");
            error.Description.Should().Be($"Referensi with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
