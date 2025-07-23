using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks.KualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.DomainTest
{
    public class KualitasIpteksBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKualitasIpteks()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.KualitasIpteks.KualitasIpteks.Create(
                Nama,
                Nilai
            );
            var KualitasIpteks = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = 10;

            KualitasIpteksBuilder builder = Domain.KualitasIpteks.KualitasIpteks.Update(KualitasIpteks);
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

            var createResult = Domain.KualitasIpteks.KualitasIpteks.Create(
                Nama,
                Nilai
            );
            var KualitasIpteks = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = -110;

            KualitasIpteksBuilder builder = Domain.KualitasIpteks.KualitasIpteks.Update(KualitasIpteks);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KualitasIpteks.InvalidValueNilai");
            result.Error.Description.Should().Be("Invalid value 'nilai'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
