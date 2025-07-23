using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori;
using Xunit;

namespace UnpakSipaksi.Modules.KategoriLuaran.DomainTest
{
    public class KategoriLuaranErrorsTests
    {
        [Theory]
        [InlineData("KategoriLuaran.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KategoriLuaran.KategoriNotFound", "Kategori is not found", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KategoriLuaran.EmptyData" => KategoriLuaranErrors.EmptyData(),
                "KategoriLuaran.KategoriNotFound" => KategoriLuaranErrors.KategoriNotFound(),
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
            var error = KategoriLuaranErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KategoriLuaran.NotFound");
            error.Description.Should().Be($"KategoriLuaran with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
