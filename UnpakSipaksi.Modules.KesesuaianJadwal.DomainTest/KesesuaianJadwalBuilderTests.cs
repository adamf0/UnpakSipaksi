using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal.KesesuaianJadwal;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.DomainTest
{
    public class KesesuaianJadwalBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKesesuaianJadwal()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.KesesuaianJadwal.KesesuaianJadwal.Create(
                Nama,
                Nilai
            );
            var KesesuaianJadwal = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = 10;

            KesesuaianJadwalBuilder builder = Domain.KesesuaianJadwal.KesesuaianJadwal.Update(KesesuaianJadwal);
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

            var createResult = Domain.KesesuaianJadwal.KesesuaianJadwal.Create(
                Nama,
                Nilai
            );
            var KesesuaianJadwal = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = -110;

            KesesuaianJadwalBuilder builder = Domain.KesesuaianJadwal.KesesuaianJadwal.Update(KesesuaianJadwal);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KesesuaianJadwal.InvalidValueNilai");
            result.Error.Description.Should().Be("Invalid value 'nilai'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
