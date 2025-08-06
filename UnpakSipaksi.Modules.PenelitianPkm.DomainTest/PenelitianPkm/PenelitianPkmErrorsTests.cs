using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.DomainTest.PenelitianPkm
{
    public class PenelitianPkmErrorsTests
    {
        [Theory]
        [InlineData("PenelitianPkm.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("PenelitianPkm.InvalidKategoriPengabdian", "Don't recognize kategori pengabdian value", ErrorType.NotFound)]
        [InlineData("PenelitianPkm.InvalidProgramPengabdian", "Don't recognize program pengabdian value", ErrorType.NotFound)]
        [InlineData("PenelitianPkm.InvalidInputRumpunIlmu", "Rumpun ilmu must be filled in", ErrorType.NotFound)]
        [InlineData("PenelitianPkm.InvalidStatusMember", "Don't recognize status command sent", ErrorType.NotFound)]
        [InlineData("PenelitianPkm.InvalidTahunPengajuan", "Tahun pengajuan is invalid format date", ErrorType.NotFound)]
        [InlineData("PenelitianPkm.InvalidStatus", "Status is invalid format", ErrorType.NotFound)]
        [InlineData("PenelitianPkm.InvalidData", "Hibah penelitian is not match existing data", ErrorType.NotFound)]
        public void StaticErrors_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "PenelitianPkm.EmptyData" => PenelitianPkmErrors.EmptyData(),
                "PenelitianPkm.InvalidKategoriPengabdian" => PenelitianPkmErrors.InvalidKategoriPengabdian(),
                "PenelitianPkm.InvalidProgramPengabdian" => PenelitianPkmErrors.InvalidProgramPengabdian(),
                "PenelitianPkm.InvalidInputRumpunIlmu" => PenelitianPkmErrors.InvalidInputRumpunIlmu(),
                "PenelitianPkm.InvalidStatusMember" => PenelitianPkmErrors.InvalidStatusMember(),
                "PenelitianPkm.InvalidTahunPengajuan" => PenelitianPkmErrors.InvalidTahunPengajuan(),
                "PenelitianPkm.InvalidStatus" => PenelitianPkmErrors.InvalidStatus(),
                "PenelitianPkm.InvalidData" => PenelitianPkmErrors.InvalidData(),
                _ => throw new ArgumentException("Unknown error code", nameof(expectedCode))
            };

            // Assert
            error.Code.Should().Be(expectedCode);
            error.Description.Should().Be(expectedDescription);
            error.Type.Should().Be(expectedType);
        }

        [Fact]
        public void NotUniqueError_ShouldReturnCorrectValues()
        {
            // Act
            var nidn = "065117251";
            var judul = "tes";
            var error = PenelitianPkmErrors.NotUnique(nidn, judul);

            // Assert
            error.Code.Should().Be("PenelitianPkm.NotUnique");
            var expectedDesc = $"Nidn {nidn} with Judul {judul} is not unique";
            error.Description.Should().Be(expectedDesc);
            error.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public void NotFound_ShouldReturnCorrectValues()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var error = PenelitianPkmErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("PenelitianPkm.NotFound");
            error.Description.Should().Be($"Penelitian hibah with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
