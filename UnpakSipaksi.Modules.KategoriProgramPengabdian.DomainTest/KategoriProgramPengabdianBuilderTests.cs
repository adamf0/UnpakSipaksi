using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian;
using static UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian.KategoriProgramPengabdian;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.DomainTest
{
    public class KategoriProgramPengabdianBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKategoriProgramPengabdian()
        {
            // Arrange
            string Nama = "ABC";
            string Rule = "[]";

            var createResult = Domain.KategoriProgramPengabdian.KategoriProgramPengabdian.Create(
                Nama,
                Rule
            );
            var KategoriProgramPengabdian = createResult.Value;

            // Act
            string newNama = "ACC";
            string newRule= "[{}]";

            KategoriProgramPengabdianBuilder builder = Domain.KategoriProgramPengabdian.KategoriProgramPengabdian.Update(KategoriProgramPengabdian);
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
        [InlineData("{}", "KategoriProgramPengabdian.InvalidFormatRule", "rule is invalid format", ErrorType.NotFound)]
        [InlineData("-", "KategoriProgramPengabdian.InvalidFormatRule", "rule is invalid format", ErrorType.NotFound)]
        [InlineData("123", "KategoriProgramPengabdian.InvalidFormatRule", "rule is invalid format", ErrorType.NotFound)]
        public void Build_WhenRuleIsInvalid_ShouldReturnFailure(string invalidRule, string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Arrange
            string Nama = "ABC";
            string Rule = "[]";

            var createResult = Domain.KategoriProgramPengabdian.KategoriProgramPengabdian.Create(
                Nama,
                Rule
            );
            var KategoriProgramPengabdian = createResult.Value;

            // Act
            string newNama = "ACC";

            KategoriProgramPengabdianBuilder builder = Domain.KategoriProgramPengabdian.KategoriProgramPengabdian.Update(KategoriProgramPengabdian);
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
