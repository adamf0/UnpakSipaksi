using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema;

namespace UnpakSipaksi.Modules.KategoriSkema.DomainTest
{
    public class KategoriSkemaTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";
            string rule = "[]";

            // Act
            var result = Domain.KategoriSkema.KategoriSkema.Create(
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
            var result = Domain.KategoriSkema.KategoriSkema.Create(
                nama,
                rule
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is KategoriSkemaCreatedDomainEvent);
        }

        [Theory]
        [InlineData("{}", "KategoriSkema.InvalidFormatRule", "rule is invalid format", ErrorType.NotFound)]
        [InlineData("-", "KategoriSkema.InvalidFormatRule", "rule is invalid format", ErrorType.NotFound)]
        [InlineData("123", "KategoriSkema.InvalidFormatRule", "rule is invalid format", ErrorType.NotFound)]
        public void Create_WithInvalidRule_ShouldReturnExpectedValidationError(string invalidRule, string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.KategoriSkema.KategoriSkema.Create(
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
