using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis.KetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.DomainTest
{
    public class KetajamanAnalisisBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKetajamanAnalisis()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.KetajamanAnalisis.KetajamanAnalisis.Create(
                Nama,
                Nilai
            );
            var KetajamanAnalisis = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = 10;

            KetajamanAnalisisBuilder builder = Domain.KetajamanAnalisis.KetajamanAnalisis.Update(KetajamanAnalisis);
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
        public void Build_WithInvalidSkor_ShouldReturnExpectedValidationError()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.KetajamanAnalisis.KetajamanAnalisis.Create(
                Nama,
                Nilai
            );
            var KetajamanAnalisis = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = -110;

            KetajamanAnalisisBuilder builder = Domain.KetajamanAnalisis.KetajamanAnalisis.Update(KetajamanAnalisis);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KetajamanAnalisis.InvalidValueNilai");
            result.Error.Description.Should().Be("Invalid value 'nilai'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
