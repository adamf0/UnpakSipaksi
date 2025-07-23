using FluentAssertions;
using UnpakSipaksi.Modules.KategoriSumberDana.Domain.KategoriSumberDana;

namespace UnpakSipaksi.Modules.KategoriSumberDana.DomainTest
{
    public class KategoriSumberDanaTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.KategoriSumberDana.KategoriSumberDana.Create(
                nama
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(nama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldRaiseKategoriCreatedDomainEvent()
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.KategoriSumberDana.KategoriSumberDana.Create(
                nama
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is KategoriSumberDanaCreatedDomainEvent);
        }
    }
}
