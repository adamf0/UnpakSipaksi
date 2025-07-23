using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian;
using Xunit;
using static UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian.FokusPenelitian;

namespace UnpakSipaksi.Modules.FokusPenelitian.DomainTest
{
    public class FokusPenelitianBuilderTests
    {
        [Fact]
        public void Change_ShouldUpdateSuccessfully()
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
