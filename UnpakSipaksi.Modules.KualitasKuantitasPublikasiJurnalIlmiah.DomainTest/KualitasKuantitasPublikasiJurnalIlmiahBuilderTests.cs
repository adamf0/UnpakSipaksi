using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.DomainTest
{
    public class KualitasKuantitasPublikasiJurnalIlmiahBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKualitasKuantitasPublikasiJurnalIlmiah()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah.Create(
                Nama,
                Nilai
            );
            var KualitasKuantitasPublikasiJurnalIlmiah = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = 10;

            KualitasKuantitasPublikasiJurnalIlmiahBuilder builder = Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah.Update(KualitasKuantitasPublikasiJurnalIlmiah);
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

            var createResult = Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah.Create(
                Nama,
                Nilai
            );
            var KualitasKuantitasPublikasiJurnalIlmiah = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = -110;

            KualitasKuantitasPublikasiJurnalIlmiahBuilder builder = Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah.Update(KualitasKuantitasPublikasiJurnalIlmiah);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KualitasKuantitasPublikasiJurnalIlmiah.InvalidValueNilai");
            result.Error.Description.Should().Be("Invalid value 'nilai'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
