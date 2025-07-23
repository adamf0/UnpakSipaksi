using FluentAssertions;
using UnpakSipaksi.Modules.Rirn.Domain.Rirn;

namespace UnpakSipaksi.Modules.Rirn.Domaintest
{
    public class RirnTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.Rirn.Rirn.Create(
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
            var result = Domain.Rirn.Rirn.Create(
                nama
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is RirnCreatedDomainEvent);
        }
    }
}
