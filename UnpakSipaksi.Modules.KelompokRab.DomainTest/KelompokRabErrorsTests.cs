using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokRab.Domain.KelompokRab;

namespace UnpakSipaksi.Modules.KelompokRab.DomainTest
{
    public class KelompokRabErrorsTests
    {
        [Theory]
        [InlineData("KelompokRab.EmptyData", "data is not found", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KelompokRab.EmptyData" => KelompokRabErrors.EmptyData(),
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
            var error = KelompokRabErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KelompokRab.NotFound");
            error.Description.Should().Be($"Kelompok rab with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
