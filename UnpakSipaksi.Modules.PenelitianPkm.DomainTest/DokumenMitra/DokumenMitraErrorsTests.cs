using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenMitra;

namespace UnpakSipaksi.Modules.PenelitianPkm.DomainTest.DokumenMitra
{
    public class DokumenMitraErrorsTests
    {
        [Theory]
        [InlineData("DokumenMitra.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("DokumenMitra.EmptyResource", "Resource is not found", ErrorType.NotFound)]
        [InlineData("DokumenMitra.InvalidData", "Hibah penelitian is not match existing data", ErrorType.NotFound)]
        [InlineData("PenelitianPkm.InvalidKelompokMitra", "Don't recognize kelompok mitra value", ErrorType.NotFound)]
        [InlineData("DokumenMitra.InvalidMitra", "Mitra name is required and must be at least 2 characters.", ErrorType.NotFound)]
        [InlineData("DokumenMitra.InvalidProvinsi", "Provinsi is required.", ErrorType.NotFound)]
        [InlineData("DokumenMitra.InvalidKota", "Kota is required.", ErrorType.NotFound)]
        [InlineData("DokumenMitra.InvalidPemimpinMitra", "Pemimpin Mitra is required and must be at least 2 characters.", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "DokumenMitra.EmptyData" => DokumenMitraErrors.EmptyData(),
                "DokumenMitra.EmptyResource" => DokumenMitraErrors.EmptyResource(),
                "DokumenMitra.InvalidData" => DokumenMitraErrors.InvalidData(),
                "PenelitianPkm.InvalidKelompokMitra" => DokumenMitraErrors.InvalidKelompokMitra(),
                "DokumenMitra.InvalidMitra" => DokumenMitraErrors.InvalidMitra(),
                "DokumenMitra.InvalidProvinsi" => DokumenMitraErrors.InvalidProvinsi(),
                "DokumenMitra.InvalidKota" => DokumenMitraErrors.InvalidKota(),
                "DokumenMitra.InvalidPemimpinMitra" => DokumenMitraErrors.InvalidPemimpinMitra(),
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
            var error = DokumenMitraErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("DokumenMitra.NotFound");
            error.Description.Should().Be($"DokumenMitra with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public void NotFoundPkm_ShouldReturnCorrectError()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var error = DokumenMitraErrors.NotFoundPkm(id);

            // Assert
            error.Code.Should().Be("DokumenMitra.NotFoundPkm");
            error.Description.Should().Be($"Pkm with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
