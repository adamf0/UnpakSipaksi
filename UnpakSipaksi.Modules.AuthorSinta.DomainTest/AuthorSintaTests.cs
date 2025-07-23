using FluentAssertions;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta;
using static UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta.AuthorSinta;

namespace UnpakSipaksi.Modules.AuthorSinta.DomainTest
{
    public class AuthorSintaTests
    {
        [Fact]
        public void Create_ShouldReturnAuthorSintaWithCorrectProperties()
        {
            // Arrange
            string nidn = "ABC";
            string authorId = "1234567";
            int score = 100;

            // Act
            var result = Domain.AuthorSinta.AuthorSinta.Create(
                nidn,
                authorId,
                score
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nidn.Should().Be(nidn);
            result.Value.AuthorId.Should().Be(authorId);
            result.Value.Score.Should().Be(score);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Theory]
        [InlineData("AuthorSinta.InvalidAuthorId", "Author identity is invalid format", ErrorType.NotFound, "111", null, 100)]
        [InlineData("AuthorSinta.InvalidAuthorId", "Author identity is invalid format", ErrorType.NotFound, "111", "123", 100)]
        [InlineData("AuthorSinta.InvalidAuthorId", "Author identity is invalid format", ErrorType.NotFound, "111", "12345678", 100)]
        [InlineData("AuthorSinta.InvalidSkor", "Skor is invalid format", ErrorType.NotFound, "111", "1234567", -1)]
        public void Create_ShouldFail_WhenInvalidData(string expectedCode, string expectedDescription, ErrorType expectedType, string nidn, string? authorId, int score)
        {
            // Arrange

            // Act
            var result = Domain.AuthorSinta.AuthorSinta.Create(
                nidn,
                authorId,
                score
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(expectedCode);
            result.Error.Description.Should().Be(expectedDescription);
            result.Error.Type.Should().Be(expectedType);
        }

        [Fact]
        public void Create_ShouldRaiseAuthorSintaCreatedDomainEvent()
        {
            // Arrange
            string nidn = "ABC";
            string authorId = "1234567";
            int score = 100;

            // Act
            var result = Domain.AuthorSinta.AuthorSinta.Create(
                nidn,
                authorId,
                score
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is AuthorSintaCreatedDomainEvent);
        }

        [Fact]
        public void Update_ShouldReturnUpdatedAuthorSinta()
        {
            // Arrange
            string nidn = "ABC";
            string authorId = "1234567";
            int score = 100;

            var createResult = Domain.AuthorSinta.AuthorSinta.Create(nidn, authorId, score);
            var authorSinta = createResult.Value;

            // Act
            string nidnChange = "AAA";
            string authorIdChange = "2345678";
            int scoreChange = 11;

            AuthorSintaBuilder builder = Domain.AuthorSinta.AuthorSinta.Update(authorSinta);
            builder.ChangeNIDN(nidnChange);
            builder.ChangeAuthorId(authorIdChange);
            builder.ChangeScore(scoreChange);
            var updatedAuthorSinta = builder.Build();

            // Assert
            updatedAuthorSinta.IsSuccess.Should().BeTrue();
            updatedAuthorSinta.Value.Nidn.Should().Be(nidnChange);
            updatedAuthorSinta.Value.AuthorId.Should().Be(authorIdChange);
            updatedAuthorSinta.Value.Score.Should().Be(scoreChange);
            updatedAuthorSinta.Value.Uuid.Should().NotBe(Guid.Empty);
        }
    }
}