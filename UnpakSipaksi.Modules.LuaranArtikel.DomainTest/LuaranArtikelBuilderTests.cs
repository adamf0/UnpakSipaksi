using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel.LuaranArtikel;

namespace UnpakSipaksi.Modules.LuaranArtikel.DomainTest
{
    public class LuaranArtikelBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedLuaranArtikel()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.LuaranArtikel.LuaranArtikel.Create(
                Nama,
                Nilai
            );
            var LuaranArtikel = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = 10;

            LuaranArtikelBuilder builder = Domain.LuaranArtikel.LuaranArtikel.Update(LuaranArtikel);
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

            var createResult = Domain.LuaranArtikel.LuaranArtikel.Create(
                Nama,
                Nilai
            );
            var LuaranArtikel = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = -110;

            LuaranArtikelBuilder builder = Domain.LuaranArtikel.LuaranArtikel.Update(LuaranArtikel);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("LuaranArtikel.InvalidValueNilai");
            result.Error.Description.Should().Be("Invalid value 'nilai'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
