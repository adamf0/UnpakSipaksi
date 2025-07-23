using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema;
using Xunit;

namespace UnpakSipaksi.Modules.KategoriSkema.DomainTest
{
    public class KategoriSkemaErrorsTests
    {
        [Theory]
        [InlineData("KategoriSkema.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KategoriSkema.InvalidFormatRule", "rule is invalid format", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KategoriSkema.EmptyData" => KategoriSkemaErrors.EmptyData(),
                "KategoriSkema.InvalidFormatRule" => KategoriSkemaErrors.InvalidFormatRule(),
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
            var error = KategoriSkemaErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KategoriSkema.NotFound");
            error.Description.Should().Be($"Kategori skema with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
