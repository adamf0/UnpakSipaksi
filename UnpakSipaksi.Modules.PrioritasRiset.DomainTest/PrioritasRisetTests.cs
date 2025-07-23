using FluentAssertions;
using UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.DomainTest
{
    public class PrioritasRisetTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.PrioritasRiset.PrioritasRiset.Create(
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
            var result = Domain.PrioritasRiset.PrioritasRiset.Create(
                nama
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is PrioritasRisetCreatedDomainEvent);
        }
    }
}
