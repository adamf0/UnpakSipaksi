using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2;

namespace UnpakSipaksi.Modules.RumpunIlmu2.DomainTest
{
    public class RumpunIlmu2ErrorsTests
    {
        [Theory]
        [InlineData("RumpunIlmu2.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("RumpunIlmu2.UnknownRumpunIlmu1", "Unknown rumpun ilmu 1", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "RumpunIlmu2.EmptyData" => RumpunIlmu2Errors.EmptyData(),
                "RumpunIlmu2.UnknownRumpunIlmu1" => RumpunIlmu2Errors.UnknownRumpunIlmu1(),
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
            var error = RumpunIlmu2Errors.NotFound(id);

            // Assert
            error.Code.Should().Be("RumpunIlmu2.NotFound");
            error.Description.Should().Be($"Rumpun Ilmu 2 with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
