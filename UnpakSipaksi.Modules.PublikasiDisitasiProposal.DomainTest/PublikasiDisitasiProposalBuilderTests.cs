using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.DomainTest
{
    public class PublikasiDisitasiProposalBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedPublikasiDisitasiProposal()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal.Create(
                Nama,
                Skor
            );
            var PublikasiDisitasiProposal = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = 10;

            PublikasiDisitasiProposalBuilder builder = Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal.Update(PublikasiDisitasiProposal);
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

            var createResult = Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal.Create(
                Nama,
                Skor
            );
            var PublikasiDisitasiProposal = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = -110;

            PublikasiDisitasiProposalBuilder builder = Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal.Update(PublikasiDisitasiProposal);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("PublikasiDisitasiProposal.InvalidValueSkor");
            result.Error.Description.Should().Be("Invalid value 'skor'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
