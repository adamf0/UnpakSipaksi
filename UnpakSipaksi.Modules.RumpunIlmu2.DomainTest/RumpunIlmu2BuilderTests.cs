using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2.RumpunIlmu2;

namespace UnpakSipaksi.Modules.RumpunIlmu2.DomainTest
{
    public class RumpunIlmu2BuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedRumpunIlmu2()
        {
            // Arrange
            string Nama = "ABC";
            int RumpunIlmu1Id = 1;

            var createResult = Domain.RumpunIlmu2.RumpunIlmu2.Create(
                Nama,
                RumpunIlmu1Id
            );
            var RumpunIlmu2 = createResult.Value;

            // Act
            string newNama = "ACC";
            int newRumpunIlmu1Id = 10;

            RumpunIlmu2Builder builder = Domain.RumpunIlmu2.RumpunIlmu2.Update(RumpunIlmu2);
            builder.ChangeNama(newNama)
                .ChangeIdRumpunIlmu1(newRumpunIlmu1Id);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}
