using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.Rirn.Domain.Rirn.Rirn;

namespace UnpakSipaksi.Modules.Rirn.Domaintest
{
    public class RirnBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedRirn()
        {
            // Arrange
            string Nama = "ABC";

            var createResult = Domain.Rirn.Rirn.Create(
                Nama
            );
            var Rirn = createResult.Value;

            // Act
            string newNama = "ACC";

            RirnBuilder builder = Domain.Rirn.Rirn.Update(Rirn);
            builder.ChangeNama(newNama);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}
