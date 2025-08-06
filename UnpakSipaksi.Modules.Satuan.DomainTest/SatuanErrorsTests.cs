using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Satuan.Domain.Satuan;

namespace UnpakSipaksi.Modules.Satuan.DomainTest
{
    public class SatuanErrorsTests
    {
        [Theory]
        [InlineData("Satuan.EmptyData", "data is not found", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "Satuan.EmptyData" => SatuanErrors.EmptyData(),
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
            var error = SatuanErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("Satuan.NotFound");
            error.Description.Should().Be($"Satuan with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
