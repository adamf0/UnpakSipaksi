using FluentAssertions;
using static UnpakSipaksi.Modules.KelompokMitra.Domain.KelompokMitra.KelompokMitra;

namespace UnpakSipaksi.Modules.KelompokMitra.DomainTest
{
    public class KelompokMitraBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKelompokMitra()
        {
            // Arrange
            string Nama = "ABC";

            var createResult = Domain.KelompokMitra.KelompokMitra.Create(
                Nama
            );
            var KelompokMitra = createResult.Value;

            // Act
            string newNama = "ACC";

            KelompokMitraBuilder builder = Domain.KelompokMitra.KelompokMitra.Update(KelompokMitra);
            builder.ChangeNama(newNama);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}
