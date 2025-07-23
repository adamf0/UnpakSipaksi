using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt.KesesuaianTkt;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Domaintest
{
    public class KesesuaianTktBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKesesuaianTkt()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.KesesuaianTkt.KesesuaianTkt.Create(
                Nama,
                Skor
            );
            var KesesuaianTkt = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = 10;

            KesesuaianTktBuilder builder = Domain.KesesuaianTkt.KesesuaianTkt.Update(KesesuaianTkt);
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

            var createResult = Domain.KesesuaianTkt.KesesuaianTkt.Create(
                Nama,
                Skor
            );
            var KesesuaianTkt = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = -110;

            KesesuaianTktBuilder builder = Domain.KesesuaianTkt.KesesuaianTkt.Update(KesesuaianTkt);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KesesuaianTkt.InvalidValueSkor");
            result.Error.Description.Should().Be("Invalid value 'skor'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
