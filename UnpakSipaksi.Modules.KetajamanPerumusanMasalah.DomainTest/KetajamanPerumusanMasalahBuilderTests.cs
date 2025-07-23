using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.DomainTest
{
    public class KetajamanPerumusanMasalahBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKetajamanPerumusanMasalah()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah.Create(
                Nama,
                Skor
            );
            var KetajamanPerumusanMasalah = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = 10;

            KetajamanPerumusanMasalahBuilder builder = Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah.Update(KetajamanPerumusanMasalah);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Skor.Should().Be(newSkor);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Build_WithInvalidSkor_ShouldReturnExpectedValidationError()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah.Create(
                Nama,
                Skor
            );
            var KetajamanPerumusanMasalah = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = -110;

            KetajamanPerumusanMasalahBuilder builder = Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah.Update(KetajamanPerumusanMasalah);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KetajamanPerumusanMasalah.InvalidValueSkor");
            result.Error.Description.Should().Be("Invalid value 'skor'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
