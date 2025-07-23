using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.DomainTest
{
    public class MetodeRencanaKegiatanBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedMetodeRencanaKegiatan()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan.Create(
                Nama,
                Nilai
            );
            var MetodeRencanaKegiatan = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = 10;

            MetodeRencanaKegiatanBuilder builder = Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan.Update(MetodeRencanaKegiatan);
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

            var createResult = Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan.Create(
                Nama,
                Nilai
            );
            var MetodeRencanaKegiatan = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = -110;

            MetodeRencanaKegiatanBuilder builder = Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan.Update(MetodeRencanaKegiatan);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("MetodeRencanaKegiatan.InvalidValueNilai");
            result.Error.Description.Should().Be("Invalid value 'nilai'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
