using FluentAssertions;
using UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2;

namespace UnpakSipaksi.Modules.RumpunIlmu2.DomainTest
{
    public class RumpunIlmu2Tests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";
            int rumpunIlmu1Id = 1;

            // Act
            var result = Domain.RumpunIlmu2.RumpunIlmu2.Create(
                nama,
                rumpunIlmu1Id
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(nama);
            result.Value.IdRumpunIlmu1.Should().Be(rumpunIlmu1Id);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldRaiseKategoriCreatedDomainEvent()
        {
            // Arrange
            string nama = "ABC";
            int rumpunIlmu1Id = 1;

            // Act
            var result = Domain.RumpunIlmu2.RumpunIlmu2.Create(
                nama,
                rumpunIlmu1Id
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is RumpunIlmu2CreatedDomainEvent);
        }
    }
}
