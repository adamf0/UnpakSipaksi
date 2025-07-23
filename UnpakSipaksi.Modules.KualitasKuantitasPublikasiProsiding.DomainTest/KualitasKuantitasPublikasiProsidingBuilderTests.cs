using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.DomainTest
{
    public class KualitasKuantitasPublikasiProsidingBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKualitasKuantitasPublikasiProsiding()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding.Create(
                Nama,
                Nilai
            );
            var KualitasKuantitasPublikasiProsiding = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = 10;

            KualitasKuantitasPublikasiProsidingBuilder builder = Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding.Update(KualitasKuantitasPublikasiProsiding);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Nilai.Should().Be(newNilai);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Build_WithInvalidNilai_ShouldReturnExpectedValidationError()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding.Create(
                Nama,
                Nilai
            );
            var KualitasKuantitasPublikasiProsiding = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = -110;

            KualitasKuantitasPublikasiProsidingBuilder builder = Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding.Update(KualitasKuantitasPublikasiProsiding);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KualitasKuantitasPublikasiProsiding.InvalidValueNilai");
            result.Error.Description.Should().Be("Invalid value 'nilai'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
