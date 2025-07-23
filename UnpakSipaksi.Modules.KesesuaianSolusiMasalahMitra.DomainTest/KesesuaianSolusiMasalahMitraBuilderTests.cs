using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.DomainTest
{
    public class KesesuaianSolusiMasalahMitraBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKesesuaianSolusiMasalahMitra()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra.Create(
                Nama,
                Nilai
            );
            var KesesuaianSolusiMasalahMitra = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = 10;

            KesesuaianSolusiMasalahMitraBuilder builder = Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra.Update(KesesuaianSolusiMasalahMitra);
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

            var createResult = Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra.Create(
                Nama,
                Nilai
            );
            var KesesuaianSolusiMasalahMitra = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = -110;

            KesesuaianSolusiMasalahMitraBuilder builder = Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra.Update(KesesuaianSolusiMasalahMitra);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KesesuaianSolusiMasalahMitra.InvalidValueNilai");
            result.Error.Description.Should().Be("Invalid value 'nilai'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
