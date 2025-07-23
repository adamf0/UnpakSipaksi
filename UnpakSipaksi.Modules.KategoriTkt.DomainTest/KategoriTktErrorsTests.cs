using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt;

namespace UnpakSipaksi.Modules.KategoriTkt.DomainTest
{
    public class KategoriTktErrorsTests
    {
        [Theory]
        [InlineData("KategoriTkt.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KategoriTkt.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KategoriTkt.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KategoriTkt.EmptyData" => KategoriTktErrors.EmptyData(),
                "KategoriTkt.UnknownKategoriSkema" => KategoriTktErrors.UnknownKategoriSkema(),
                "KategoriTkt.NotSameValue" => KategoriTktErrors.NotSameValue(),
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
            var error = KategoriTktErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KategoriTkt.NotFound");
            error.Description.Should().Be($"Kategori tkt with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
