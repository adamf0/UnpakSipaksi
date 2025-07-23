using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.DomainTest
{
    public class RelevansiProdukKepentinganNasionalErrorsTests
    {
        [Theory]
        [InlineData("RelevansiProdukKepentinganNasional.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("RelevansiProdukKepentinganNasional.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("RelevansiProdukKepentinganNasional.InvalidValueSkor", "Invalid value 'skor'", ErrorType.NotFound)]
        [InlineData("RelevansiProdukKepentinganNasional.NotSameValue", "not the same value in data 'skor'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "RelevansiProdukKepentinganNasional.EmptyData" => RelevansiProdukKepentinganNasionalErrors.EmptyData(),
                "RelevansiProdukKepentinganNasional.UnknownKategoriSkema" => RelevansiProdukKepentinganNasionalErrors.UnknownKategoriSkema(),
                "RelevansiProdukKepentinganNasional.InvalidValueSkor" => RelevansiProdukKepentinganNasionalErrors.InvalidValueSkor(),
                "RelevansiProdukKepentinganNasional.NotSameValue" => RelevansiProdukKepentinganNasionalErrors.NotSameValue(),
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
            var error = RelevansiProdukKepentinganNasionalErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("RelevansiProdukKepentinganNasional.NotFound");
            error.Description.Should().Be($"Relevansi produk kepentingan nasional with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
