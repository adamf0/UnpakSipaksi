using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.Referensi.Domain.Referensi.Referensi;

namespace UnpakSipaksi.Modules.Referensi.DomainTest
{
    public class ReferensiBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedReferensi()
        {
            // Arrange
            string Nama = "abc";
            int KebaruanReferensiId = 1;
            int RelevansiKualitasReferensiId = 2;
            int Nilai = 10;

            var createResult = Domain.Referensi.Referensi.Create(
                Nama,
                KebaruanReferensiId,
                RelevansiKualitasReferensiId,
                Nilai
            );
            var Referensi = createResult.Value;

            // Act
            string newNama = "ABC";
            int newKebaruanReferensiId = 10;
            int newRelevansiKualitasReferensiId = 20;
            int newNilai = 100;

            ReferensiBuilder builder = Domain.Referensi.Referensi.Update(Referensi);
            builder.ChangeNama(newNama)
                    .ChangeKebaruanReferensiId(newKebaruanReferensiId)
                    .ChangeRelevansiKualitasReferensiId(newRelevansiKualitasReferensiId)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.KebaruanReferensiId.Should().Be(newKebaruanReferensiId);
            result.Value.RelevansiKualitasReferensiId.Should().Be(newRelevansiKualitasReferensiId);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Theory]
        [InlineData("Referensi.NotFoundKebaruanReferensiId", "data kebaruanReferensiId is not found", ErrorType.NotFound, "abc", -1, 1, 1)]
        [InlineData("Referensi.NotFoundRelevansiKualitasReferensiId", "data relevansiKualitasReferensiId is not found", ErrorType.NotFound, "abc", 1, -1, 1)]
        [InlineData("Referensi.InvalidValueNilai", "Invalid value 'nilai'", ErrorType.NotFound, "abc", 1, 1, -1)]
        public void Build_WithInvalidData_ShouldReturnExpectedValidationError(
            string expectedCode, 
            string expectedDescription, 
            ErrorType expectedType,
            string newNama,
            int newKebaruanReferensiId,
            int newRelevansiKualitasReferensiId,
            int newNilai
        )
        {
            // Arrange
            string Nama = "abc";
            int KebaruanReferensiId = 1;
            int RelevansiKualitasReferensiId = 2;
            int Nilai = 10;

            var createResult = Domain.Referensi.Referensi.Create(
                Nama,
                KebaruanReferensiId,
                RelevansiKualitasReferensiId,
                Nilai
            );
            var Referensi = createResult.Value;

            // Act
            ReferensiBuilder builder = Domain.Referensi.Referensi.Update(Referensi);
            builder.ChangeNama(newNama)
                    .ChangeKebaruanReferensiId(newKebaruanReferensiId)
                    .ChangeRelevansiKualitasReferensiId(newRelevansiKualitasReferensiId)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(expectedCode);
            result.Error.Description.Should().Be(expectedDescription);
            result.Error.Type.Should().Be(expectedType);
        }
    }
}
