using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.DomainTest
{
    public class RelevansiKualitasReferensiBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedRelevansiKualitasReferensi()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi.Create(
                Nama,
                Skor
            );
            var RelevansiKualitasReferensi = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = 10;

            RelevansiKualitasReferensiBuilder builder = Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi.Update(RelevansiKualitasReferensi);
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

            var createResult = Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi.Create(
                Nama,
                Skor
            );
            var RelevansiKualitasReferensi = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = -110;

            RelevansiKualitasReferensiBuilder builder = Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi.Update(RelevansiKualitasReferensi);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("RelevansiKualitasReferensi.InvalidValueSkor");
            result.Error.Description.Should().Be("Invalid value 'skor'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
