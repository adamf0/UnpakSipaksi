using FluentAssertions;
using UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt;

namespace UnpakSipaksi.Modules.KategoriTkt.DomainTest
{
    public class KategoriTktTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.KategoriTkt.KategoriTkt.Create(
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
            var result = Domain.KategoriTkt.KategoriTkt.Create(
                nama
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is KategoriTktCreatedDomainEvent);
        }
    }
}
