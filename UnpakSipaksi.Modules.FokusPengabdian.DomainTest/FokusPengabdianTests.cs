using FluentAssertions;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian;
using static UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian.FokusPengabdian;

namespace UnpakSipaksi.Modules.FokusPengabdian.DomainTest
{
    public class FokusPengabdianTests
    {
        [Fact]
        public void Create_ShouldReturnFokusPengabdianWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.FokusPengabdian.FokusPengabdian.Create(
                nama
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(nama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_ShouldRaiseFokusPengabdianCreatedDomainEvent()
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.FokusPengabdian.FokusPengabdian.Create(
                nama
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is FokusPengabdianCreatedDomainEvent);
        }

        [Fact]
        public void Update_ShouldReturnUpdatedFokusPengabdian()
        {
            // Arrange
            string nama = "ABC";

            var createResult = Domain.FokusPengabdian.FokusPengabdian.Create(nama);
            var fokusPenelitian = createResult.Value;

            // Act
            string namaChange = "AAA";

            FokusPengabdianBuilder builder = Domain.FokusPengabdian.FokusPengabdian.Update(fokusPenelitian);
            builder.ChangeNama(namaChange);
            var updatedFokusPengabdian = builder.Build();

            // Assert
            updatedFokusPengabdian.IsSuccess.Should().BeTrue();
            updatedFokusPengabdian.Value.Nama.Should().Be(namaChange);
            updatedFokusPengabdian.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}