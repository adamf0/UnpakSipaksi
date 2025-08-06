using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1;

namespace UnpakSipaksi.Modules.RumpunIlmu1.DomainTest
{
    public class RumpunIlmu1ErrorsTests
    {
        [Theory]
        [InlineData("RumpunIlmu1.EmptyData", "data is not found", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "RumpunIlmu1.EmptyData" => RumpunIlmu1Errors.EmptyData(),
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
            var error = RumpunIlmu1Errors.NotFound(id);

            // Assert
            error.Code.Should().Be("RumpunIlmu1.NotFound");
            error.Description.Should().Be($"Rumpun Ilmu 1 with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
