using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokMitra.Domain.KelompokMitra;

namespace UnpakSipaksi.Modules.KelompokMitra.DomainTest
{
    public class KelompokMitraErrorsTests
    {
        [Theory]
        [InlineData("KelompokMitra.EmptyData", "data is not found", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KelompokMitra.EmptyData" => KelompokMitraErrors.EmptyData(),
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
            var error = KelompokMitraErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KelompokMitra.NotFound");
            error.Description.Should().Be($"Kelompok mitra with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
