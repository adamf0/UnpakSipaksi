using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi;
using Xunit;
using static UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.DomainTest
{
    public class JumlahKolaboratorPublikasBereputasiBuilderTests
    {
        [Fact]
        public void Change_ShouldUpdateSuccessfully()
        {
            // Arrange
            string Nama = "AA";
            int Skor = 1;

            var createResult = Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi.Create(Nama, Skor);
            var inovasiPemecahanMasalah = createResult.Value;

            // Act
            string namaChange = "ABC";
            int skorChange = 100;
            int bobotKerjasamaChange = 1;
            int bobotPdpChange = 2;
            int bobotTerapanChange = 3;
            int bobotPenelitianDasarChange = 4;

            JumlahKolaboratorPublikasBereputasiBuilder builder = Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi.Update(inovasiPemecahanMasalah);
            builder.ChangeNama(namaChange);
            builder.ChangeSkor(skorChange);
            builder.ChangeBobotKerjasama(1);
            builder.ChangeBobotPDP(2);
            builder.ChangeBobotTerapan(3);
            builder.ChangeBobotPenelitianDasar(4);
            var updatedJumlahKolaboratorPublikasBereputasi = builder.Build();

            // Assert
            updatedJumlahKolaboratorPublikasBereputasi.IsSuccess.Should().BeTrue();
            updatedJumlahKolaboratorPublikasBereputasi.Value.Nama.Should().Be(namaChange);
            updatedJumlahKolaboratorPublikasBereputasi.Value.Skor.Should().Be(skorChange);
            updatedJumlahKolaboratorPublikasBereputasi.Value.BobotKerjasama.Should().Be(bobotKerjasamaChange);
            updatedJumlahKolaboratorPublikasBereputasi.Value.BobotPDP.Should().Be(bobotPdpChange);
            updatedJumlahKolaboratorPublikasBereputasi.Value.BobotTerapan.Should().Be(bobotTerapanChange);
            updatedJumlahKolaboratorPublikasBereputasi.Value.BobotPenelitianDasar.Should().Be(bobotPenelitianDasarChange);
            updatedJumlahKolaboratorPublikasBereputasi.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Theory]
        [InlineData("JumlahKolaboratorPublikasBereputasi.InvalidBobotKerjasama", "Bobot kerjasama is invalid format", ErrorType.NotFound, "ABC", 100, -1, 2, 3, 4)]
        [InlineData("JumlahKolaboratorPublikasBereputasi.InvalidBobotPDP", "Bobot pdp is invalid format", ErrorType.NotFound, "ABC", 100, 1, -2, 3, 4)]
        [InlineData("JumlahKolaboratorPublikasBereputasi.InvalidBobotTerapan", "Bobot terapan is invalid format", ErrorType.NotFound, "ABC", 100, 1, 2, -3, 4)]
        [InlineData("JumlahKolaboratorPublikasBereputasi.InvalidBobotPenelitianDasar", "Bobot penelitian dasar is invalid format", ErrorType.NotFound, "ABC", 100, 1, 2, 3, -4)]
        [InlineData("JumlahKolaboratorPublikasBereputasi.InvalidSkor", "Skor is invalid format", ErrorType.NotFound, "ABC", -100, 1, 2, 3, 4)]
        public void Build_ShouldReturnFailure_WhenInvalidChangeOccurs(
            string expectedCode,
            string expectedDescription,
            ErrorType expectedType,
            string namaChange,
            int skorChange,
            int bobotKerjasamaChange,
            int bobotPDPChange,
            int bobotTerapanChange,
            int bobotPenelitianDasarChange)
        {
            // Arrange
            string Nama = "AA";
            int Skor = 1;

            Error error = expectedCode switch
            {
                "JumlahKolaboratorPublikasBereputasi.InvalidBobotKerjasama" => JumlahKolaboratorPublikasBereputasiErrors.InvalidBobotKerjasama(),
                "JumlahKolaboratorPublikasBereputasi.InvalidBobotPDP" => JumlahKolaboratorPublikasBereputasiErrors.InvalidBobotPDP(),
                "JumlahKolaboratorPublikasBereputasi.InvalidBobotTerapan" => JumlahKolaboratorPublikasBereputasiErrors.InvalidBobotTerapan(),
                "JumlahKolaboratorPublikasBereputasi.InvalidBobotPenelitianDasar" => JumlahKolaboratorPublikasBereputasiErrors.InvalidBobotPenelitianDasar(),
                "JumlahKolaboratorPublikasBereputasi.InvalidSkor" => JumlahKolaboratorPublikasBereputasiErrors.InvalidSkor(),
                _ => throw new ArgumentException("Unknown error code", nameof(expectedCode))
            };

            var result = Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi.Create(
                Nama,
                Skor
            );

            // Act
            JumlahKolaboratorPublikasBereputasiBuilder builder = Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi.Update(result.Value);
            builder.ChangeNama(namaChange);
            builder.ChangeSkor(skorChange);
            builder.ChangeBobotKerjasama(bobotKerjasamaChange);
            builder.ChangeBobotPDP(bobotPDPChange);
            builder.ChangeBobotTerapan(bobotTerapanChange);
            builder.ChangeBobotPenelitianDasar(bobotPenelitianDasarChange);
            var updatedJumlahKolaboratorPublikasBereputasi = builder.Build();

            // Assert
            updatedJumlahKolaboratorPublikasBereputasi.IsFailure.Should().BeTrue();
            updatedJumlahKolaboratorPublikasBereputasi.Error.Code.Should().Be(expectedCode);  // Expect specific error code
            updatedJumlahKolaboratorPublikasBereputasi.Error.Description.Should().Be(expectedDescription);  // Expect specific error message
        }
    }
}
