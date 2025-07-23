using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian;
using Xunit;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.DomainTest
{
    public class KategoriProgramPengabdianErrorsTests
    {
        [Theory]
        [InlineData("KategoriProgramPengabdian.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KategoriProgramPengabdian.InvalidFormatRule", "rule is invalid format", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KategoriProgramPengabdian.EmptyData" => KategoriProgramPengabdianErrors.EmptyData(),
                "KategoriProgramPengabdian.InvalidFormatRule" => KategoriProgramPengabdianErrors.InvalidFormatRule(),
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
            var error = KategoriProgramPengabdianErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KategoriProgramPengabdian.NotFound");
            error.Description.Should().Be($"Kategori program pengabdian with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
