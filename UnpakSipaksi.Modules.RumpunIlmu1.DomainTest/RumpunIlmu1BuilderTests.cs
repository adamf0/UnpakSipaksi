using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1.RumpunIlmu1;

namespace UnpakSipaksi.Modules.RumpunIlmu1.DomainTest
{
    public class RumpunIlmu1BuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedRumpunIlmu1()
        {
            // Arrange
            string Nama = "ABC";

            var createResult = Domain.RumpunIlmu1.RumpunIlmu1.Create(
                Nama
            );
            var RumpunIlmu1 = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = 10;

            RumpunIlmu1Builder builder = Domain.RumpunIlmu1.RumpunIlmu1.Update(RumpunIlmu1);
            builder.ChangeNama(newNama);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}
