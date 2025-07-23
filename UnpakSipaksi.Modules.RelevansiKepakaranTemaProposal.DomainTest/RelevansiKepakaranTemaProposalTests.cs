using FluentAssertions;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.DomainTest
{
    public class RelevansiKepakaranTemaProposalTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";
            int skor = 1;

            // Act
            var result = Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal.Create(
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
            var result = Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal.Create(
                nama,
                skor
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is RelevansiKepakaranTemaProposalCreatedDomainEvent);
        }
    }
}
