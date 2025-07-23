using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori;
using static UnpakSipaksi.Modules.KategoriLuaran.Domain.KategoriLuaran.KategoriLuaran;

namespace UnpakSipaksi.Modules.KategoriLuaran.DomainTest
{
    public class KategoriLuaranBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKategoriLuaran()
        {
            // Arrange
            int KategoriId = 1;
            string Nama = "ABC";
            string Status = "ok";

            var createResult = Domain.KategoriLuaran.KategoriLuaran.Create(
                KategoriId,
                Nama,
                Status
            );
            var kategoriLuaran = createResult.Value;

            // Act
            int newKategoriId = 10;
            string newNama = "ACC";
            string newStatus = "";

            KategoriLuaranBuilder builder = Domain.KategoriLuaran.KategoriLuaran.Update(kategoriLuaran);
            builder.ChangeKategoriId(newKategoriId)
                   .ChangeNama(newNama)
                   .ChangeStatus(newStatus);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.KategoriId.Should().Be(newKategoriId);
            result.Value.Nama.Should().Be(newNama);
            result.Value.Status.Should().Be(newStatus);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Build_WhenKategoriIdIsInvalid_ShouldReturnFailure()
        {
            // Arrange
            int KategoriId = 1;
            string Nama = "ABC";
            string Status = "ok";

            var createResult = Domain.KategoriLuaran.KategoriLuaran.Create(
                KategoriId,
                Nama,
                Status
            );
            var kategoriLuaran = createResult.Value;

            // Act
            int invalidKategoriId = -10;
            string newNama = "ACC";
            string newStatus = "";

            KategoriLuaranBuilder builder = Domain.KategoriLuaran.KategoriLuaran.Update(kategoriLuaran);
            builder.ChangeKategoriId(invalidKategoriId)
                   .ChangeNama(newNama)
                   .ChangeStatus(newStatus);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KategoriLuaran.KategoriNotFound");
            result.Error.Description.Should().Be("Kategori is not found");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
