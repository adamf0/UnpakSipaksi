using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.DomainTest
{
    public class PotensiKetercapaianLuaranDijanjikanBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedPotensiKetercapaianLuaranDijanjikan()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan.Create(
                Nama,
                Skor
            );
            var PotensiKetercapaianLuaranDijanjikan = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = 10;

            PotensiKetercapaianLuaranDijanjikanBuilder builder = Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan.Update(PotensiKetercapaianLuaranDijanjikan);
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

            var createResult = Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan.Create(
                Nama,
                Skor
            );
            var PotensiKetercapaianLuaranDijanjikan = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = -110;

            PotensiKetercapaianLuaranDijanjikanBuilder builder = Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan.Update(PotensiKetercapaianLuaranDijanjikan);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("PotensiKetercapaianLuaranDijanjikan.InvalidValueSkor");
            result.Error.Description.Should().Be("Invalid value 'skor'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
