using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan.SotaKebaharuan;

namespace UnpakSipaksi.Modules.SotaKebaharuan.DomainTest
{
    public class SotaKebaharuanBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedSotaKebaharuan()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.SotaKebaharuan.SotaKebaharuan.Create(
                Nama,
                Skor
            );
            var SotaKebaharuan = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = 10;

            SotaKebaharuanBuilder builder = Domain.SotaKebaharuan.SotaKebaharuan.Update(SotaKebaharuan);
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

            var createResult = Domain.SotaKebaharuan.SotaKebaharuan.Create(
                Nama,
                Skor
            );
            var SotaKebaharuan = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = -110;

            SotaKebaharuanBuilder builder = Domain.SotaKebaharuan.SotaKebaharuan.Update(SotaKebaharuan);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("SotaKebaharuan.InvalidValueSkor");
            result.Error.Description.Should().Be("Invalid value 'skor'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
