using FluentAssertions;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian;
using static UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian.FokusPenelitian;

namespace UnpakSipaksi.Modules.FokusPenelitian.DomainTest
{
    public class FokusPenelitianTests
    {
        [Fact]
        public void Create_ShouldReturnFokusPenelitianWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.FokusPenelitian.FokusPenelitian.Create(
                nama
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(nama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_ShouldRaiseFokusPenelitianCreatedDomainEvent()
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.FokusPenelitian.FokusPenelitian.Create(
                nama
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is FokusPenelitianCreatedDomainEvent);
        }

        [Fact]
        public void Update_ShouldReturnUpdatedFokusPenelitian()
        {
            // Arrange
            string nama = "ABC";

            var createResult = Domain.FokusPenelitian.FokusPenelitian.Create(nama);
            var fokusPenelitian = createResult.Value;

            // Act
            string namaChange = "AAA";

            FokusPenelitianBuilder builder = Domain.FokusPenelitian.FokusPenelitian.Update(fokusPenelitian);
            builder.ChangeNama(namaChange);
            var updatedFokusPenelitian = builder.Build();

            // Assert
            updatedFokusPenelitian.IsSuccess.Should().BeTrue();
            updatedFokusPenelitian.Value.Nama.Should().Be(namaChange);
            updatedFokusPenelitian.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}