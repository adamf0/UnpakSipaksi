using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3;

namespace UnpakSipaksi.Modules.RumpunIlmu3.DomainTest
{
    public class RumpunIlmu3ErrorsTests
    {
        [Theory]
        [InlineData("RumpunIlmu3.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("RumpunIlmu3.UnknownRumpunIlmu2", "Unknown rumpun ilmu 2", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "RumpunIlmu3.EmptyData" => RumpunIlmu3Errors.EmptyData(),
                "RumpunIlmu3.UnknownRumpunIlmu2" => RumpunIlmu3Errors.UnknownRumpunIlmu2(),
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
            var error = RumpunIlmu3Errors.NotFound(id);

            // Assert
            error.Code.Should().Be("RumpunIlmu3.NotFound");
            error.Description.Should().Be($"Rumpun Ilmu 3 with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
