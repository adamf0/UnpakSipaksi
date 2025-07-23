using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.DomainTest
{
    public class PublikasiDisitasiProposalErrorsTests
    {
        [Theory]
        [InlineData("PublikasiDisitasiProposal.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("PublikasiDisitasiProposal.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("PublikasiDisitasiProposal.InvalidValueSkor", "Invalid value 'skor'", ErrorType.NotFound)]
        [InlineData("PublikasiDisitasiProposal.NotSameValue", "not the same value in data 'skor'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "PublikasiDisitasiProposal.EmptyData" => PublikasiDisitasiProposalErrors.EmptyData(),
                "PublikasiDisitasiProposal.UnknownKategoriSkema" => PublikasiDisitasiProposalErrors.UnknownKategoriSkema(),
                "PublikasiDisitasiProposal.InvalidValueSkor" => PublikasiDisitasiProposalErrors.InvalidValueSkor(),
                "PublikasiDisitasiProposal.NotSameValue" => PublikasiDisitasiProposalErrors.NotSameValue(),
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
            var error = PublikasiDisitasiProposalErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("PublikasiDisitasiProposal.NotFound");
            error.Description.Should().Be($"Publikasi disitasi proposal with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
