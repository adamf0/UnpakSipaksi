using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.DomainTest
{
    public class KualitasIpteksErrorsTests
    {
        [Theory]
        [InlineData("KualitasIpteks.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KualitasIpteks.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KualitasIpteks.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("KualitasIpteks.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KualitasIpteks.EmptyData" => KualitasIpteksErrors.EmptyData(),
                "KualitasIpteks.UnknownKategoriSkema" => KualitasIpteksErrors.UnknownKategoriSkema(),
                "KualitasIpteks.InvalidValueNilai" => KualitasIpteksErrors.InvalidValueNilai(),
                "KualitasIpteks.NotSameValue" => KualitasIpteksErrors.NotSameValue(),
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
            var error = KualitasIpteksErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KualitasIpteks.NotFound");
            error.Description.Should().Be($"Kualitas ipteks with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
