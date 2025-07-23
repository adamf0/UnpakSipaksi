using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.IndikatorCapaian.Domain;
using Xunit;

namespace UnpakSipaksi.Modules.IndikatorCapaian.DomainTest
{
    public class IndikatorCapaianErrorsTests
    {
        [Theory]
        [InlineData("IndikatorCapaian.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("IndikatorCapaian.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        [InlineData("IndikatorCapaian.UnknownJenisLuaran", "Unknown jenis luaran", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "IndikatorCapaian.EmptyData" => IndikatorCapaianErrors.EmptyData(),
                "IndikatorCapaian.NotSameValue" => IndikatorCapaianErrors.NotSameValue(),
                "IndikatorCapaian.UnknownJenisLuaran" => IndikatorCapaianErrors.UnknownJenisLuaran(),
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
            var error = IndikatorCapaianErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("IndikatorCapaian.NotFound");
            error.Description.Should().Be($"Indikator capaian with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
