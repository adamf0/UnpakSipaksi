using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi;
using Xunit;
using static UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi.JenisPublikasi;

namespace UnpakSipaksi.Modules.JenisPublikasi.DomainTest
{
    public class JenisPublikasiBuilderTests
    {
        [Fact]
        public void Change_ShouldUpdateSuccessfully()
        {
            // Arrange
            string Nama = "AA";
            int Sbu = 1;

            var createResult = Domain.JenisPublikasi.JenisPublikasi.Create(Nama, Sbu);
            var inovasiPemecahanMasalah = createResult.Value;

            // Act
            string namaChange = "ABC";
            int sbuChange = 100;

            JenisPublikasiBuilder builder = Domain.JenisPublikasi.JenisPublikasi.Update(inovasiPemecahanMasalah);
            builder.ChangeNama(namaChange);
            builder.ChangeSbu(sbuChange);
            var updatedJenisPublikasi = builder.Build();

            // Assert
            updatedJenisPublikasi.IsSuccess.Should().BeTrue();
            updatedJenisPublikasi.Value.Nama.Should().Be(namaChange);
            updatedJenisPublikasi.Value.Sbu.Should().Be(sbuChange);
            updatedJenisPublikasi.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Theory]
        [InlineData("JenisPublikasi.InvalidSbu", "Sbu invalid format", ErrorType.NotFound, "ABC", -1)]
        public void Build_ShouldReturnFailure_WhenInvalidChangeOccurs(
            string expectedCode,
            string expectedDescription,
            ErrorType expectedType,
            string namaChange,
            int sbuChange)
        {
            // Arrange
            string Nama = "AA";
            int Sbu = 1;

            Error error = expectedCode switch
            {
                "JenisPublikasi.InvalidSbu" => JenisPublikasiErrors.InvalidSbu(),
                _ => throw new ArgumentException("Unknown error code", nameof(expectedCode))
            };

            var result = Domain.JenisPublikasi.JenisPublikasi.Create(
                Nama,
                Sbu
            );

            // Act
            JenisPublikasiBuilder builder = Domain.JenisPublikasi.JenisPublikasi.Update(result.Value);
            builder.ChangeNama(namaChange);
            builder.ChangeSbu(sbuChange);
            var updatedJenisPublikasi = builder.Build();

            // Assert
            updatedJenisPublikasi.IsFailure.Should().BeTrue();
            updatedJenisPublikasi.Error.Code.Should().Be(expectedCode);  // Expect specific error code
            updatedJenisPublikasi.Error.Description.Should().Be(expectedDescription);  // Expect specific error message
        }
    }
}
