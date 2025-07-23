using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.DomainTest
{
    public class KategoriProgramPengabdianTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";
            string rule = "[]";

            // Act
            var result = Domain.KategoriProgramPengabdian.KategoriProgramPengabdian.Create(
                nama,
                rule
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(nama);
            result.Value.Rule.Should().Be(rule);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldRaiseKategoriCreatedDomainEvent()
        {
            // Arrange
            string nama = "ABC";
            string rule = "[]";

            // Act
            var result = Domain.KategoriProgramPengabdian.KategoriProgramPengabdian.Create(
                nama,
                rule
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is KategoriProgramPengabdianCreatedDomainEvent);
        }

        [Theory]
        [InlineData("{}", "KategoriProgramPengabdian.InvalidFormatRule", "rule is invalid format", ErrorType.NotFound)]
        [InlineData("-", "KategoriProgramPengabdian.InvalidFormatRule", "rule is invalid format", ErrorType.NotFound)]
        [InlineData("123", "KategoriProgramPengabdian.InvalidFormatRule", "rule is invalid format", ErrorType.NotFound)]
        public void Create_WithInvalidRule_ShouldReturnExpectedValidationError(string invalidRule, string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.KategoriProgramPengabdian.KategoriProgramPengabdian.Create(
                nama,
                invalidRule
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(expectedCode);
            result.Error.Description.Should().Be(expectedDescription);
            result.Error.Type.Should().Be(expectedType);
        }

    }
}
