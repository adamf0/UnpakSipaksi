using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.Satuan.Domain.Satuan.Satuan;

namespace UnpakSipaksi.Modules.Satuan.DomainTest
{
    public class SatuanBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedSatuan()
        {
            // Arrange
            string Nama = "ABC";

            var createResult = Domain.Satuan.Satuan.Create(
                Nama
            );
            var Satuan = createResult.Value;

            // Act
            string newNama = "ACC";

            SatuanBuilder builder = Domain.Satuan.Satuan.Update(Satuan);
            builder.ChangeNama(newNama);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}
