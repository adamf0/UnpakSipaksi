using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisPublikasi.Domain;
using UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi;
using Xunit;

namespace UnpakSipaksi.Modules.JenisPublikasi.DomainTest
{
    public class JenisPublikasiErrorsTests
    {
        [Theory]
        [InlineData("JenisPublikasi.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("JenisPublikasi.InvalidSbu", "Sbu invalid format", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "JenisPublikasi.EmptyData" => JenisPublikasiErrors.EmptyData(),
                "JenisPublikasi.InvalidSbu" => JenisPublikasiErrors.InvalidSbu(),
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
            var error = JenisPublikasiErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("JenisPublikasi.NotFound");
            error.Description.Should().Be($"Jenis publikasi with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
