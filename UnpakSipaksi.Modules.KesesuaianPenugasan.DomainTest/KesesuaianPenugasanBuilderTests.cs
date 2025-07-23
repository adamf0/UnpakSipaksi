using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan.KesesuaianPenugasan;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.DomainTest
{
    public class KesesuaianPenugasanBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKesesuaianPenugasan()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.KesesuaianPenugasan.KesesuaianPenugasan.Create(
                Nama,
                Nilai
            );
            var KesesuaianPenugasan = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = 10;

            KesesuaianPenugasanBuilder builder = Domain.KesesuaianPenugasan.KesesuaianPenugasan.Update(KesesuaianPenugasan);
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

            var createResult = Domain.KesesuaianPenugasan.KesesuaianPenugasan.Create(
                Nama,
                Nilai
            );
            var KesesuaianPenugasan = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = -110;

            KesesuaianPenugasanBuilder builder = Domain.KesesuaianPenugasan.KesesuaianPenugasan.Update(KesesuaianPenugasan);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KesesuaianPenugasan.InvalidValueNilai");
            result.Error.Description.Should().Be("Invalid value 'nilai'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
