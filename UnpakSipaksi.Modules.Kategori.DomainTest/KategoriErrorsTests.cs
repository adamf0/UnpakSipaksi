using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.Domain.Kategori;
using Xunit;

namespace UnpakSipaksi.Modules.Kategori.DomainTest
{
    public class KategoriErrorsTests
    {
        [Theory]
        [InlineData("Kategori.EmptyData", "data is not found", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "Kategori.EmptyData" => KategoriErrors.EmptyData(),
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
            var error = KategoriErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("Kategori.NotFound");
            error.Description.Should().Be($"Kategori with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
