using FluentAssertions;
using static UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt.KategoriTkt;

namespace UnpakSipaksi.Modules.KategoriTkt.DomainTest
{
    public class KategoriTktBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKategoriTkt()
        {
            // Arrange
            string Nama = "ABC";

            var createResult = Domain.KategoriTkt.KategoriTkt.Create(
                Nama
            );
            var KategoriTkt = createResult.Value;

            // Act
            string newNama = "ACC";

            KategoriTktBuilder builder = Domain.KategoriTkt.KategoriTkt.Update(KategoriTkt);
            builder.ChangeNama(newNama);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}
