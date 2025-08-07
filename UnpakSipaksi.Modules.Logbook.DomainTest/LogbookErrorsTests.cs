using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Logbook.Domain.Logbook;

namespace UnpakSipaksi.Modules.Logbook.DomainTest
{
    public class LogbookErrorsTests
    {
        [Theory]
        [InlineData("Logbook.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("Logbook.OverCapacity", "percentage over capacity", ErrorType.NotFound)]
        [InlineData("Logbook.NoTargetHibah", "not target penelitian hibah or pengabdian", ErrorType.NotFound)]
        [InlineData("Logbook.InvalidFormatPercentage", "percentage is invalid format", ErrorType.NotFound)]
        [InlineData("Logbook.InvalidFormatDate", "Tanggal kegiatan is invalid format", ErrorType.NotFound)]
        [InlineData("Logbook.InvalidData", "Hibah penelitian is not match existing data", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "Logbook.EmptyData" => LogbookErrors.EmptyData(),
                "Logbook.OverCapacity" => LogbookErrors.OverCapacity(),
                "Logbook.NoTargetHibah" => LogbookErrors.NoTargetHibah(),
                "Logbook.InvalidFormatPercentage" => LogbookErrors.InvalidFormatPercentage(),
                "Logbook.InvalidFormatDate" => LogbookErrors.InvalidFormatDate(),
                "Logbook.InvalidData" => LogbookErrors.InvalidData(),
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
            var id = Guid.NewGuid();

            // Act
            var error = LogbookErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("Logbook.NotFound");
            error.Description.Should().Be($"Kelompok mitra with the identifier {id} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
