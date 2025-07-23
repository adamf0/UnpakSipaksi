using FluentAssertions;
using Moq;
using UnpakSipaksi.Modules.AkurasiPenelitian.Domain.AkurasiPenelitian;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.DomainTest
{
    public class AkurasiPenelitianTests
    {
        [Fact]
        public void Create_ShouldReturnAkurasiPenelitianWithCorrectProperties()
        {
            // Arrange
            string nama = "AkurasiPenelitian A";
            int skor = 1000;

            // Act
            var result = Domain.AkurasiPenelitian.AkurasiPenelitian.Create(
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
        public void Create_ShouldFail_WhenSkorIsNegative()
        {
            // Arrange
            string nama = "AkurasiPenelitian A";
            int skor = -1000;

            // Act
            var result = Domain.AkurasiPenelitian.AkurasiPenelitian.Create(
                nama,
                skor
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("AkurasiPenelitian.InvalidSkor");
            result.Error.Description.Should().Be("Skor is invalid format");
        }

        [Fact]
        public void Create_ShouldRaiseAkurasiPenelitianCreatedDomainEvent()
        {
            // Arrange
            string nama = "AkurasiPenelitian A";
            int skor = 1000;

            // Act
            var komponen = Domain.AkurasiPenelitian.AkurasiPenelitian.Create(nama, skor);

            // Assert
            komponen.Value.DomainEvents.Should().Contain(e => e is AkurasiPenelitianCreatedDomainEvent);
        }

        [Fact]
        public void Update_ShouldReturnUpdatedAkurasiPenelitian()
        {
            // Arrange
            // Pertama, buat komponen menggunakan metode Create
            var komponen = Domain.AkurasiPenelitian.AkurasiPenelitian.Create("Old AkurasiPenelitian", 500).Value;

            // Act
            // Gunakan builder untuk mengupdate komponen yang ada
            var namaChange = "Updated AkurasiPenelitian";
            var skorChange = 1400;

            var builder = Domain.AkurasiPenelitian.AkurasiPenelitian.Update(komponen);
            builder.ChangeNama(namaChange);
            builder.ChangeSkor(skorChange);
            var updatedAkurasiPenelitian = builder.Build();

            // Assert
            updatedAkurasiPenelitian.IsSuccess.Should().BeTrue();
            updatedAkurasiPenelitian.Value.Nama.Should().Be(namaChange);
            updatedAkurasiPenelitian.Value.Skor.Should().Be(skorChange);
            updatedAkurasiPenelitian.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}