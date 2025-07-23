using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian;
using Xunit;
using static UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian.FokusPengabdian;

namespace UnpakSipaksi.Modules.FokusPengabdian.DomainTest
{
    public class FokusPengabdianBuilderTests
    {
        [Fact]
        public void Change_ShouldUpdateSuccessfully()
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
