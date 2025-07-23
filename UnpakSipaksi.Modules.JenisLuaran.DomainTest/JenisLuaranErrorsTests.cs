using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisLuaran.Domain;
using Xunit;

namespace UnpakSipaksi.Modules.JenisLuaran.DomainTest
{
    public class JenisLuaranErrorsTests
    {
        [Theory]
        [InlineData("JenisLuaran.EmptyData", "data is not found", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "JenisLuaran.EmptyData" => JenisLuaranErrors.EmptyData(),
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
            var error = JenisLuaranErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("JenisLuaran.NotFound");
            error.Description.Should().Be($"Jenis luaran with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
