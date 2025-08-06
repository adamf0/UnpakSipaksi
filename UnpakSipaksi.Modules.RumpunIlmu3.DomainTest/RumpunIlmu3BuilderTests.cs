using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3.RumpunIlmu3;

namespace UnpakSipaksi.Modules.RumpunIlmu3.DomainTest
{
    public class RumpunIlmu3BuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedRumpunIlmu3()
        {
            // Arrange
            string Nama = "ABC";
            int RumpunIlmu2Id = 1;

            var createResult = Domain.RumpunIlmu3.RumpunIlmu3.Create(
                Nama,
                RumpunIlmu2Id
            );
            var RumpunIlmu3 = createResult.Value;

            // Act
            string newNama = "ACC";
            int newRumpunIlmu2Id = 10;

            RumpunIlmu3Builder builder = Domain.RumpunIlmu3.RumpunIlmu3.Update(RumpunIlmu3);
            builder.ChangeNama(newNama)
                .ChangeIdRumpunIlmu2(newRumpunIlmu2Id);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}
