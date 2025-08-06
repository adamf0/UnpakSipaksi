using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan;

namespace UnpakSipaksi.Modules.VideoKegiatan.DomainTest
{
    public class VideoKegiatanErrorsTests
    {
        [Theory]
        [InlineData("VideoKegiatan.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("VideoKegiatan.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("VideoKegiatan.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("VideoKegiatan.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "VideoKegiatan.EmptyData" => VideoKegiatanErrors.EmptyData(),
                "VideoKegiatan.UnknownKategoriSkema" => VideoKegiatanErrors.UnknownKategoriSkema(),
                "VideoKegiatan.InvalidValueNilai" => VideoKegiatanErrors.InvalidValueNilai(),
                "VideoKegiatan.NotSameValue" => VideoKegiatanErrors.NotSameValue(),
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
            var error = VideoKegiatanErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("VideoKegiatan.NotFound");
            error.Description.Should().Be($"Video kegiatan with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
