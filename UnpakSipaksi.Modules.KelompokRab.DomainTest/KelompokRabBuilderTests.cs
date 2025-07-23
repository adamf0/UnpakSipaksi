using FluentAssertions;
using static UnpakSipaksi.Modules.KelompokRab.Domain.KelompokRab.KelompokRab;

namespace UnpakSipaksi.Modules.KelompokRab.DomainTest
{
    public class KelompokRabBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKelompokRab()
        {
            // Arrange
            string Nama = "ABC";

            var createResult = Domain.KelompokRab.KelompokRab.Create(
                Nama
            );
            var KelompokRab = createResult.Value;

            // Act
            string newNama = "ACC";

            KelompokRabBuilder builder = Domain.KelompokRab.KelompokRab.Update(KelompokRab);
            builder.ChangeNama(newNama);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}
