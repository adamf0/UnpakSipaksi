using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta;
using Xunit;
using static UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta.AuthorSinta;

namespace UnpakSipaksi.Modules.AuthorSinta.DomainTest
{
    public class AuthorSintaBuilderTests
    {
        [Fact]
        public void Change_ShouldUpdateSuccessfully()
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

        [Theory]
        [InlineData("AuthorSinta.InvalidAuthorId", "Author identity is invalid format", ErrorType.NotFound, "111", null, 100)]
        [InlineData("AuthorSinta.InvalidAuthorId", "Author identity is invalid format", ErrorType.NotFound, "111", "123", 100)]
        [InlineData("AuthorSinta.InvalidAuthorId", "Author identity is invalid format", ErrorType.NotFound, "111", "12345678", 100)]
        [InlineData("AuthorSinta.InvalidSkor", "Skor is invalid format", ErrorType.NotFound, "111", "1234567", -1)]
        public void Build_ShouldReturnFailure_WhenInvalidChangeOccurs(string expectedCode, string expectedDescription, ErrorType expectedType, string nidnChange, string? authorIdChange, int scoreChange)
        {
            // Arrange
            string nidn = "ABC";
            string authorId = "1234567";
            int score = 100;

            Error error = expectedCode switch
            {
                "AuthorSinta.InvalidAuthorId" => AuthorSintaErrors.InvalidAuthorId(),
                "AuthorSinta.InvalidSkor" => AuthorSintaErrors.InvalidSkor(),
                _ => throw new ArgumentException("Unknown error code", nameof(expectedCode))
            };

            var result = Domain.AuthorSinta.AuthorSinta.Create(
                nidn,
                authorId,
                score
            );

            // Act
            var builder = new Domain.AuthorSinta.AuthorSinta.AuthorSintaBuilder(result.Value);
            builder.ChangeNIDN(nidnChange);
            builder.ChangeAuthorId(authorIdChange);
            builder.ChangeScore(scoreChange);
            var updatedAuthorSinta = builder.Build();

            // Assert
            updatedAuthorSinta.IsFailure.Should().BeTrue();
            updatedAuthorSinta.Error.Code.Should().Be(expectedCode);  // Expect specific error code
            updatedAuthorSinta.Error.Description.Should().Be(expectedDescription);  // Expect specific error message
        }
    }
}
