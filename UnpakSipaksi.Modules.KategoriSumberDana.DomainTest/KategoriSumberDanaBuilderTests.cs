using FluentAssertions;
using static UnpakSipaksi.Modules.KategoriSumberDana.Domain.KategoriSumberDana.KategoriSumberDana;

namespace UnpakSipaksi.Modules.KategoriSumberDana.DomainTest
{
    public class KategoriSumberDanaBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKategoriSumberDana()
        {
            // Arrange
            string Nama = "ABC";

            var createResult = Domain.KategoriSumberDana.KategoriSumberDana.Create(
                Nama
            );
            var KategoriSumberDana = createResult.Value;

            // Act
            string newNama = "ACC";

            KategoriSumberDanaBuilder builder = Domain.KategoriSumberDana.KategoriSumberDana.Update(KategoriSumberDana);
            builder.ChangeNama(newNama);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}
