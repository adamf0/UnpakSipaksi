using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.DomainTest
{
    public class KesesuaianJadwalErrorsTests
    {
        [Theory]
        [InlineData("KesesuaianJadwal.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KesesuaianJadwal.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KesesuaianJadwal.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound)]
        [InlineData("KesesuaianJadwal.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KesesuaianJadwal.EmptyData" => KesesuaianJadwalErrors.EmptyData(),
                "KesesuaianJadwal.UnknownKategoriSkema" => KesesuaianJadwalErrors.UnknownKategoriSkema(),
                "KesesuaianJadwal.InvalidValueNilai" => KesesuaianJadwalErrors.InvalidValueNilai(),
                "KesesuaianJadwal.NotSameValue" => KesesuaianJadwalErrors.NotSameValue(),
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
            var error = KesesuaianJadwalErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KesesuaianJadwal.NotFound");
            error.Description.Should().Be($"Kesesuaian jadwal with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
