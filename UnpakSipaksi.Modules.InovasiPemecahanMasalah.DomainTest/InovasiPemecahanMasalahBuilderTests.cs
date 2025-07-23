using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah;
using Xunit;
using static UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.DomainTest
{
    public class InovasiPemecahanMasalahBuilderTests
    {
        [Fact]
        public void Change_ShouldUpdateSuccessfully()
        {
            // Arrange
            string Nama = "AA";
            int Skor = 1;

            var createResult = Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah.Create(Nama, Skor);
            var inovasiPemecahanMasalah = createResult.Value;

            // Act
            string namaChange = "ABC";
            int skorChange = 100;
            int bobotKerjasamaChange = 1;
            int bobotPdpChange = 2;
            int bobotTerapanChange = 3;
            int bobotPenelitianDasarChange = 4;

            InovasiPemecahanMasalahBuilder builder = Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah.Update(inovasiPemecahanMasalah);
            builder.ChangeNama(namaChange);
            builder.ChangeSkor(skorChange);
            builder.ChangeBobotKerjasama(1);
            builder.ChangeBobotPDP(2);
            builder.ChangeBobotTerapan(3);
            builder.ChangeBobotPenelitianDasar(4);
            var updatedInovasiPemecahanMasalah = builder.Build();

            // Assert
            updatedInovasiPemecahanMasalah.IsSuccess.Should().BeTrue();
            updatedInovasiPemecahanMasalah.Value.Nama.Should().Be(namaChange);
            updatedInovasiPemecahanMasalah.Value.Skor.Should().Be(skorChange);
            updatedInovasiPemecahanMasalah.Value.BobotKerjasama.Should().Be(bobotKerjasamaChange);
            updatedInovasiPemecahanMasalah.Value.BobotPDP.Should().Be(bobotPdpChange);
            updatedInovasiPemecahanMasalah.Value.BobotTerapan.Should().Be(bobotTerapanChange);
            updatedInovasiPemecahanMasalah.Value.BobotPenelitianDasar.Should().Be(bobotPenelitianDasarChange);
            updatedInovasiPemecahanMasalah.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Theory]
        [InlineData("InovasiPemecahanMasalah.InvalidBobotKerjasama", "Bobot kerjasama is invalid format", ErrorType.NotFound, "ABC", 100, -1, 2, 3, 4)]
        [InlineData("InovasiPemecahanMasalah.InvalidBobotPDP", "Bobot pdp is invalid format", ErrorType.NotFound, "ABC", 100, 1, -2, 3, 4)]
        [InlineData("InovasiPemecahanMasalah.InvalidBobotTerapan", "Bobot terapan is invalid format", ErrorType.NotFound, "ABC", 100, 1, 2, -3, 4)]
        [InlineData("InovasiPemecahanMasalah.InvalidBobotPenelitianDasar", "Bobot penelitian dasar is invalid format", ErrorType.NotFound, "ABC", 100, 1, 2, 3, -4)]
        [InlineData("InovasiPemecahanMasalah.InvalidSkor", "Skor is invalid format", ErrorType.NotFound, "ABC", -100, 1, 2, 3, 4)]
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
                "InovasiPemecahanMasalah.InvalidBobotKerjasama" => InovasiPemecahanMasalahErrors.InvalidBobotKerjasama(),
                "InovasiPemecahanMasalah.InvalidBobotPDP" => InovasiPemecahanMasalahErrors.InvalidBobotPDP(),
                "InovasiPemecahanMasalah.InvalidBobotTerapan" => InovasiPemecahanMasalahErrors.InvalidBobotTerapan(),
                "InovasiPemecahanMasalah.InvalidBobotPenelitianDasar" => InovasiPemecahanMasalahErrors.InvalidBobotPenelitianDasar(),
                "InovasiPemecahanMasalah.InvalidSkor" => InovasiPemecahanMasalahErrors.InvalidSkor(),
                _ => throw new ArgumentException("Unknown error code", nameof(expectedCode))
            };

            var result = Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah.Create(
                Nama,
                Skor
            );

            // Act
            InovasiPemecahanMasalahBuilder builder = Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah.Update(result.Value);
            builder.ChangeNama(namaChange);
            builder.ChangeSkor(skorChange);
            builder.ChangeBobotKerjasama(bobotKerjasamaChange);
            builder.ChangeBobotPDP(bobotPDPChange);
            builder.ChangeBobotTerapan(bobotTerapanChange);
            builder.ChangeBobotPenelitianDasar(bobotPenelitianDasarChange);
            var updatedInovasiPemecahanMasalah = builder.Build();

            // Assert
            updatedInovasiPemecahanMasalah.IsFailure.Should().BeTrue();
            updatedInovasiPemecahanMasalah.Error.Code.Should().Be(expectedCode);  // Expect specific error code
            updatedInovasiPemecahanMasalah.Error.Description.Should().Be(expectedDescription);  // Expect specific error message
        }
    }
}
