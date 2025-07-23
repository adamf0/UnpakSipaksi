using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget.KewajaranTahapanTarget;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.DomainTest
{
    public class KewajaranTahapanTargetBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKewajaranTahapanTarget()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.KewajaranTahapanTarget.KewajaranTahapanTarget.Create(
                Nama,
                Nilai
            );
            var KewajaranTahapanTarget = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = 10;

            KewajaranTahapanTargetBuilder builder = Domain.KewajaranTahapanTarget.KewajaranTahapanTarget.Update(KewajaranTahapanTarget);
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

            var createResult = Domain.KewajaranTahapanTarget.KewajaranTahapanTarget.Create(
                Nama,
                Nilai
            );
            var KewajaranTahapanTarget = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = -110;

            KewajaranTahapanTargetBuilder builder = Domain.KewajaranTahapanTarget.KewajaranTahapanTarget.Update(KewajaranTahapanTarget);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KewajaranTahapanTarget.InvalidValueNilai");
            result.Error.Description.Should().Be("Invalid value 'nilai'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
