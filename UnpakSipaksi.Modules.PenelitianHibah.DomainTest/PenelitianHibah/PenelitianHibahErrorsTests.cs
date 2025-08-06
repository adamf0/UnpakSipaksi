using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.DomainTest.PenelitianHibah
{
    public class PenelitianHibahErrorsTests
    {
        [Theory]
        [InlineData("PenelitianHibah.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("PenelitianHibah.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        [InlineData("PenelitianHibah.InvalidInputPrioritasFokus", "Prioritas riset / Bidang fokus penelitian must be filled in", ErrorType.NotFound)]
        [InlineData("PenelitianHibah.InvalidFormatTahunPengajuan", "Invalid format tahun pengajuan", ErrorType.NotFound)]
        [InlineData("PenelitianHibah.InvalidInputRumpunIlmu", "Rumpun ilmu must be filled in", ErrorType.NotFound)]
        [InlineData("PenelitianHibah.InvalidStatusMember", "Don't recognize status command sent", ErrorType.NotFound)]
        [InlineData("PenelitianHibah.InvalidTahunPengajuan", "Tahun pengajuan is invalid format date", ErrorType.NotFound)]
        [InlineData("PenelitianHibah.InvalidStatus", "Status is invalid format", ErrorType.NotFound)]
        [InlineData("PenelitianHibah.InvalidData", "Hibah penelitian is not match existing data", ErrorType.NotFound)]
        public void StaticErrors_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "PenelitianHibah.EmptyData" => PenelitianHibahErrors.EmptyData(),
                "PenelitianHibah.NotSameValue" => PenelitianHibahErrors.NotSameValue(),
                "PenelitianHibah.InvalidInputPrioritasFokus" => PenelitianHibahErrors.InvalidInputPrioritasFokus(),
                "PenelitianHibah.InvalidFormatTahunPengajuan" => PenelitianHibahErrors.InvalidFormatTahunPengajuan(),
                "PenelitianHibah.InvalidInputRumpunIlmu" => PenelitianHibahErrors.InvalidInputRumpunIlmu(),
                "PenelitianHibah.InvalidStatusMember" => PenelitianHibahErrors.InvalidStatusMember(),
                "PenelitianHibah.InvalidTahunPengajuan" => PenelitianHibahErrors.InvalidTahunPengajuan(),
                "PenelitianHibah.InvalidStatus" => PenelitianHibahErrors.InvalidStatus(),
                "PenelitianHibah.InvalidData" => PenelitianHibahErrors.InvalidData(),
                _ => throw new ArgumentException("Unknown error code", nameof(expectedCode))
            };

            // Assert
            error.Code.Should().Be(expectedCode);
            error.Description.Should().Be(expectedDescription);
            error.Type.Should().Be(expectedType);
        }

        [Theory]
        [InlineData("065117251", "tes", 1, ErrorType.NotFound)]
        [InlineData("065117251", "tes", 2, ErrorType.NotFound)]
        public void NotUniqueError_ShouldReturnCorrectValues(string nidn, string judul, int type, ErrorType expectedType)
        {
            // Act
            var error = type == 1
                ? PenelitianHibahErrors.NotUnique(nidn)
                : PenelitianHibahErrors.NotUnique(nidn, judul);

            // Assert
            error.Code.Should().Be("PenelitianHibah.NotUnique");
            var expectedDesc = type == 1
                ? $"Nidn {nidn} is not unique"
                : $"Nidn {nidn} with Judul {judul} is not unique";
            error.Description.Should().Be(expectedDesc);
            error.Type.Should().Be(expectedType);
        }

        [Theory]
        [InlineData("1", "KategoriSkema", ErrorType.NotFound)]
        [InlineData("1", "KategoriTkt", ErrorType.NotFound)]
        public void NotFoundKategori_ShouldReturnCorrectValues(string id, string type, ErrorType expectedType)
        {
            // Act
            var error = type == "KategoriSkema"
                ? PenelitianHibahErrors.NotFoundKategoriSkema(id)
                : PenelitianHibahErrors.NotFoundKategoriTkt(id);

            // Assert
            error.Code.Should().Be(type == "KategoriSkema"? "PenelitianHibah.NotFoundKategoriSkema": "PenelitianHibah.NotFoundKategoriTkt");
            var expectedDesc = type == "KategoriSkema"
                ? $"Kategori skema {id} is not found"
                : $"Kategori tkt {id} is not found";
            error.Description.Should().Be(expectedDesc);
            error.Type.Should().Be(expectedType);
        }

        [Fact]
        public void NotFound_ShouldReturnCorrectValues()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var error = PenelitianHibahErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("PenelitianHibah.NotFound");
            error.Description.Should().Be($"Penelitian hibah with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
