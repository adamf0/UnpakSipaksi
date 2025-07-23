using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.DomainTest
{
    public class RelevansiKepakaranTemaProposalBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedRelevansiKepakaranTemaProposal()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal.Create(
                Nama,
                Skor
            );
            var RelevansiKepakaranTemaProposal = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = 10;

            RelevansiKepakaranTemaProposalBuilder builder = Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal.Update(RelevansiKepakaranTemaProposal);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Skor.Should().Be(newSkor);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Build_WithInvalidSkor_ShouldReturnExpectedValidationError()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal.Create(
                Nama,
                Skor
            );
            var RelevansiKepakaranTemaProposal = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = -110;

            RelevansiKepakaranTemaProposalBuilder builder = Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal.Update(RelevansiKepakaranTemaProposal);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("RelevansiKepakaranTemaProposal.InvalidValueSkor");
            result.Error.Description.Should().Be("Invalid value 'skor'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
