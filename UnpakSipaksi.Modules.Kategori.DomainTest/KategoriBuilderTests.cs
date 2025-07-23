using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.Domain.Kategori;
using Xunit;
using static UnpakSipaksi.Modules.Kategori.Domain.Kategori.Kategori;

namespace UnpakSipaksi.Modules.Kategori.DomainTest
{
    public class KategoriBuilderTests
    {
        [Fact]
        public void Change_ShouldUpdateSuccessfully()
        {
            // Arrange
            string Nama = "AA";

            var createResult = Domain.Kategori.Kategori.Create(Nama);
            var inovasiPemecahanMasalah = createResult.Value;

            // Act
            string namaChange = "ABC";

            KategoriBuilder builder = Domain.Kategori.Kategori.Update(inovasiPemecahanMasalah);
            builder.ChangeNama(namaChange);
            var updatedKategori = builder.Build();

            // Assert
            updatedKategori.IsSuccess.Should().BeTrue();
            updatedKategori.Value.Nama.Should().Be(namaChange);
            updatedKategori.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}
