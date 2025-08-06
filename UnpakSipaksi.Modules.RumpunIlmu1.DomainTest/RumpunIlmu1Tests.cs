using FluentAssertions;
using UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1;

namespace UnpakSipaksi.Modules.RumpunIlmu1.DomainTest
{
    public class RumpunIlmu1Tests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.RumpunIlmu1.RumpunIlmu1.Create(
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
            var result = Domain.RumpunIlmu1.RumpunIlmu1.Create(
                nama
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is RumpunIlmu1CreatedDomainEvent);
        }
    }
}
