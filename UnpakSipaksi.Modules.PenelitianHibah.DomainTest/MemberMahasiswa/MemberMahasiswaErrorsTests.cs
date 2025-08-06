using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberMahasiswa;

namespace UnpakSipaksi.Modules.PenelitianHibah.DomainTest.MemberMahasiswa
{
    public class MemberMahasiswaErrorsTests
    {
        [Theory]
        [InlineData("MemberMahasiswa.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("MemberMahasiswa.NotFoundHibah", "Penelitian hibah not found", ErrorType.NotFound)]
        [InlineData("MemberMahasiswa.InvalidUrlBuktiMbkm", "Bukti mbkm is invalid url", ErrorType.NotFound)]
        [InlineData("MemberMahasiswa.InvalidHostBuktiMbkm", "Bukti mbkm is invalid host url", ErrorType.NotFound)]
        [InlineData("MemberMahasiswa.InvalidNpm", "Npm is invalid format", ErrorType.NotFound)]
        public void StaticErrors_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "MemberMahasiswa.EmptyData" => MemberMahasiswaErrors.EmptyData(),
                "MemberMahasiswa.NotFoundHibah" => MemberMahasiswaErrors.NotFoundHibah(),
                "MemberMahasiswa.InvalidUrlBuktiMbkm" => MemberMahasiswaErrors.InvalidUrlBuktiMbkm(),
                "MemberMahasiswa.InvalidHostBuktiMbkm" => MemberMahasiswaErrors.InvalidHostBuktiMbkm(),
                "MemberMahasiswa.InvalidNpm" => MemberMahasiswaErrors.InvalidNpm(),
                _ => throw new ArgumentException("Unknown error code", nameof(expectedCode))
            };

            // Assert
            error.Code.Should().Be(expectedCode);
            error.Description.Should().Be(expectedDescription);
            error.Type.Should().Be(expectedType);
        }

        [Fact]
        public void NotUnique_ShouldReturnCorrectMessage()
        {
            var error = MemberMahasiswaErrors.NotUnique("12345678");
            error.Code.Should().Be("MemberMahasiswa.NotUnique");
            error.Description.Should().Be("NPM 12345678 is not unique");
            error.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public void NotFound_ShouldReturnCorrectMessage()
        {
            var id = Guid.NewGuid();
            var error = MemberMahasiswaErrors.NotFound(id);
            error.Code.Should().Be("MemberMahasiswa.NotFound");
            error.Description.Should().Be($"Member hibah mahasiswa with identifier {id} not found");
        }

        [Fact]
        public void NotFoundHibah_ShouldReturnCorrectMessage()
        {
            var id = Guid.NewGuid();
            var error = MemberMahasiswaErrors.NotFoundHibah(id);
            error.Code.Should().Be("MemberMahasiswa.NotFoundHibah");
            error.Description.Should().Be($"Penelitian hibah with identifier {id} not found");
        }
    }
}
