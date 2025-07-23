using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema;
using static UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema.KategoriSkema;

namespace UnpakSipaksi.Modules.KategoriSkema.DomainTest
{
    public class KategoriSkemaBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKategoriSkema()
        {
            // Arrange
            string Nama = "ABC";
            string Rule = "[]";

            var createResult = Domain.KategoriSkema.KategoriSkema.Create(
                Nama,
                Rule
            );
            var KategoriSkema = createResult.Value;

            // Act
            string newNama = "ACC";
            string newRule = "[{}]";

            KategoriSkemaBuilder builder = Domain.KategoriSkema.KategoriSkema.Update(KategoriSkema);
            builder.ChangeNama(newNama)
                   .ChangeRule(newRule);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Rule.Should().Be(newRule);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Theory]
        [InlineData("{}", "KategoriSkema.InvalidFormatRule", "rule is invalid format", ErrorType.NotFound)]
        [InlineData("-", "KategoriSkema.InvalidFormatRule", "rule is invalid format", ErrorType.NotFound)]
        [InlineData("123", "KategoriSkema.InvalidFormatRule", "rule is invalid format", ErrorType.NotFound)]
        public void Build_WhenRuleIsInvalid_ShouldReturnFailure(string invalidRule, string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Arrange
            string Nama = "ABC";
            string Rule = "[]";

            var createResult = Domain.KategoriSkema.KategoriSkema.Create(
                Nama,
                Rule
            );
            var KategoriSkema = createResult.Value;

            // Act
            string newNama = "ACC";

            KategoriSkemaBuilder builder = Domain.KategoriSkema.KategoriSkema.Update(KategoriSkema);
            builder.ChangeNama(newNama)
                   .ChangeRule(invalidRule);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(expectedCode);
            result.Error.Description.Should().Be(expectedDescription);
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
