using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSumberDana.Domain.KategoriSumberDana;

namespace UnpakSipaksi.Modules.KategoriSumberDana.DomainTest
{
    public class KategoriSumberDanaErrorsTests
    {
        [Theory]
        [InlineData("KategoriSumberDana.EmptyData", "data is not found", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KategoriSumberDana.EmptyData" => KategoriSumberDanaErrors.EmptyData(),
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
            var error = KategoriSumberDanaErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KategoriSumberDana.NotFound");
            error.Description.Should().Be($"Kategori sumber dana with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
