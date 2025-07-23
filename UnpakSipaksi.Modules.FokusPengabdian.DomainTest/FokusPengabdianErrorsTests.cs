using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian;
using Xunit;

namespace UnpakSipaksi.Modules.FokusPengabdian.DomainTest
{
    public class FokusPengabdianErrorsTests
    {
        [Theory]
        [InlineData("FokusPengabdian.EmptyData", "data is not found", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "FokusPengabdian.EmptyData" => FokusPengabdianErrors.EmptyData(),
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
            var error = FokusPengabdianErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("FokusPengabdian.NotFound");
            error.Description.Should().Be($"Fokus pengabdian with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
