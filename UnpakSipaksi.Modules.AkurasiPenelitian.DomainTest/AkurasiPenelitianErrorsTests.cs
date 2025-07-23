using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AkurasiPenelitian.Domain.AkurasiPenelitian;
using Xunit;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.DomainTest
{
    public class AkurasiPenelitianErrorsTests
    {
        [Theory]
        [InlineData("AkurasiPenelitian.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("AkurasiPenelitian.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        [InlineData("AkurasiPenelitian.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "AkurasiPenelitian.EmptyData" => AkurasiPenelitianErrors.EmptyData(),
                "AkurasiPenelitian.NotSameValue" => AkurasiPenelitianErrors.NotSameValue(),
                "AkurasiPenelitian.UnknownKategoriSkema" => AkurasiPenelitianErrors.UnknownKategoriSkema(),
                _ => throw new ArgumentException("Unknown error code", nameof(expectedCode))
            };

            // Assert
            error.Code.Should().Be(expectedCode);
            error.Description.Should().Be(expectedDescription);
            error.Type.Should().Be(expectedType);
        }

        [Fact]
        public void NotFound_ShouldReturnCorrectError()
        {
            // Arrange
            var akurasiPenelitianId = Guid.NewGuid();

            // Act
            var error = AkurasiPenelitianErrors.NotFound(akurasiPenelitianId);

            // Assert
            error.Code.Should().Be("AkurasiPenelitian.NotFound");
            error.Description.Should().Be($"Akurasi penelitian with identifier {akurasiPenelitianId} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
