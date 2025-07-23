using FluentAssertions;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.DomainTest
{
    public class ModelFeasibilityStudysTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";
            int skor = 1;

            // Act
            var result = Domain.ModelFeasibilityStudys.ModelFeasibilityStudys.Create(
                nama,
                skor
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(nama);
            result.Value.Skor.Should().Be(skor);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldRaiseKategoriCreatedDomainEvent()
        {
            // Arrange
            string nama = "ABC";
            int skor = 1;

            // Act
            var result = Domain.ModelFeasibilityStudys.ModelFeasibilityStudys.Create(
                nama,
                skor
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is ModelFeasibilityStudysCreatedDomainEvent);
        }
    }
}
