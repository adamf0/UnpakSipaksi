using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset.PrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.DomainTest
{
    public class PrioritasRisetBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedPrioritasRiset()
        {
            // Arrange
            string Nama = "ABC";

            var createResult = Domain.PrioritasRiset.PrioritasRiset.Create(
                Nama
            );
            var PrioritasRiset = createResult.Value;

            // Act
            string newNama = "ACC";

            PrioritasRisetBuilder builder = Domain.PrioritasRiset.PrioritasRiset.Update(PrioritasRiset);
            builder.ChangeNama(newNama);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}
