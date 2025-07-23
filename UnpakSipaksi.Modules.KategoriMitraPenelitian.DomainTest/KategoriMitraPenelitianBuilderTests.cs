using FluentAssertions;
using static UnpakSipaksi.Modules.KategoriMitraPenelitian.Domain.KategoriMitraPenelitian.KategoriMitraPenelitian;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.DomainTest
{
    public class KategoriMitraPenelitianBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKategoriMitraPenelitian()
        {
            // Arrange
            string Nama = "ABC";

            var createResult = Domain.KategoriMitraPenelitian.KategoriMitraPenelitian.Create(
                Nama
            );
            var KategoriMitraPenelitian = createResult.Value;

            // Act
            string newNama = "ACC";

            KategoriMitraPenelitianBuilder builder = Domain.KategoriMitraPenelitian.KategoriMitraPenelitian.Update(KategoriMitraPenelitian);
            builder.ChangeNama(newNama);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}
