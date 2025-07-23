using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.DomainTest
{
    public class RelevansiKepakaranTemaProposalErrorsTests
    {
        [Theory]
        [InlineData("RelevansiKepakaranTemaProposal.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("RelevansiKepakaranTemaProposal.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("RelevansiKepakaranTemaProposal.InvalidValueSkor", "Invalid value 'skor'", ErrorType.NotFound)]
        [InlineData("RelevansiKepakaranTemaProposal.NotSameValue", "not the same value in data 'skor'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "RelevansiKepakaranTemaProposal.EmptyData" => RelevansiKepakaranTemaProposalErrors.EmptyData(),
                "RelevansiKepakaranTemaProposal.UnknownKategoriSkema" => RelevansiKepakaranTemaProposalErrors.UnknownKategoriSkema(),
                "RelevansiKepakaranTemaProposal.InvalidValueSkor" => RelevansiKepakaranTemaProposalErrors.InvalidValueSkor(),
                "RelevansiKepakaranTemaProposal.NotSameValue" => RelevansiKepakaranTemaProposalErrors.NotSameValue(),
                _ => throw new ArgumentException("Unknown error code", nameof(expectedCode))
            };

            // Assert
            error.Code.Should().Be(expectedCode);
            error.Description.Should().Be(expectedDescription);
            error.Type.Should().Be(expectedType);
        }

        [Fact]
        public void NotFound_ShouldReturnCorrectError()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var error = RelevansiKepakaranTemaProposalErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("RelevansiKepakaranTemaProposal.NotFound");
            error.Description.Should().Be($"Relevansi kepakaran tema proposal with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
