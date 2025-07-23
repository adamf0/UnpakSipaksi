using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi.KebaruanReferensi;

namespace UnpakSipaksi.Modules.KebaruanReferensi.DomainTest
{
    public class KebaruanReferensiBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKebaruanReferensi()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.KebaruanReferensi.KebaruanReferensi.Create(
                Nama,
                Skor
            );
            var KebaruanReferensi = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = 10;

            KebaruanReferensiBuilder builder = Domain.KebaruanReferensi.KebaruanReferensi.Update(KebaruanReferensi);
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

            var createResult = Domain.KebaruanReferensi.KebaruanReferensi.Create(
                Nama,
                Skor
            );
            var KebaruanReferensi = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = -110;

            KebaruanReferensiBuilder builder = Domain.KebaruanReferensi.KebaruanReferensi.Update(KebaruanReferensi);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KebaruanReferensi.InvalidValueSkor");
            result.Error.Description.Should().Be("Invalid value 'skor'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
