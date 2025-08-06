using FluentAssertions;
using UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3;

namespace UnpakSipaksi.Modules.RumpunIlmu3.DomainTest
{
    public class RumpunIlmu3Tests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";
            int rumpunIlmu2Id = 1;

            // Act
            var result = Domain.RumpunIlmu3.RumpunIlmu3.Create(
                nama,
                rumpunIlmu2Id
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(nama);
            result.Value.IdRumpunIlmu2.Should().Be(rumpunIlmu2Id);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldRaiseKategoriCreatedDomainEvent()
        {
            // Arrange
            string nama = "ABC";
            int rumpunIlmu2Id = 1;

            // Act
            var result = Domain.RumpunIlmu3.RumpunIlmu3.Create(
                nama,
                rumpunIlmu2Id
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is RumpunIlmu3CreatedDomainEvent);
        }
    }
}
