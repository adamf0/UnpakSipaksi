using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Domain.KategoriMitraPenelitian;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.DomainTest
{
    public class KategoriMitraPenelitianErrorsTests
    {
        [Theory]
        [InlineData("KategoriMitraPenelitian.EmptyData", "data is not found", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KategoriMitraPenelitian.EmptyData" => KategoriMitraPenelitianErrors.EmptyData(),
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
            var error = KategoriMitraPenelitianErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KategoriMitraPenelitian.NotFound");
            error.Description.Should().Be($"Kategori mitra penelitian with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
