using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.DomainTest
{
    public class PeningkatanKeberdayaanMitraErrorsTests
    {
        [Theory]
        [InlineData("PeningkatanKeberdayaanMitra.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("PeningkatanKeberdayaanMitra.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("PeningkatanKeberdayaanMitra.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("PeningkatanKeberdayaanMitra.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "PeningkatanKeberdayaanMitra.EmptyData" => PeningkatanKeberdayaanMitraErrors.EmptyData(),
                "PeningkatanKeberdayaanMitra.UnknownKategoriSkema" => PeningkatanKeberdayaanMitraErrors.UnknownKategoriSkema(),
                "PeningkatanKeberdayaanMitra.InvalidValueNilai" => PeningkatanKeberdayaanMitraErrors.InvalidValueNilai(),
                "PeningkatanKeberdayaanMitra.NotSameValue" => PeningkatanKeberdayaanMitraErrors.NotSameValue(),
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
            var error = PeningkatanKeberdayaanMitraErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("PeningkatanKeberdayaanMitra.NotFound");
            error.Description.Should().Be($"Peningkatan keberdayaan mitra with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
