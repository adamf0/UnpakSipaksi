using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.DomainTest
{
    public class KuantitasStatusKiErrorsTests
    {
        [Theory]
        [InlineData("KuantitasStatusKi.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KuantitasStatusKi.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KuantitasStatusKi.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("KuantitasStatusKi.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KuantitasStatusKi.EmptyData" => KuantitasStatusKiErrors.EmptyData(),
                "KuantitasStatusKi.UnknownKategoriSkema" => KuantitasStatusKiErrors.UnknownKategoriSkema(),
                "KuantitasStatusKi.InvalidValueNilai" => KuantitasStatusKiErrors.InvalidValueNilai(),
                "KuantitasStatusKi.NotSameValue" => KuantitasStatusKiErrors.NotSameValue(),
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
            var error = KuantitasStatusKiErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KuantitasStatusKi.NotFound");
            error.Description.Should().Be($"Kuantitas status KI with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
