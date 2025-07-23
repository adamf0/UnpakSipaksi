using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi.KuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.DomainTest
{
    public class KuantitasStatusKiBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKuantitasStatusKi()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.KuantitasStatusKi.KuantitasStatusKi.Create(
                Nama,
                Nilai
            );
            var KuantitasStatusKi = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = 10;

            KuantitasStatusKiBuilder builder = Domain.KuantitasStatusKi.KuantitasStatusKi.Update(KuantitasStatusKi);
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

            var createResult = Domain.KuantitasStatusKi.KuantitasStatusKi.Create(
                Nama,
                Nilai
            );
            var KuantitasStatusKi = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = -110;

            KuantitasStatusKiBuilder builder = Domain.KuantitasStatusKi.KuantitasStatusKi.Update(KuantitasStatusKi);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KuantitasStatusKi.InvalidValueNilai");
            result.Error.Description.Should().Be("Invalid value 'nilai'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
