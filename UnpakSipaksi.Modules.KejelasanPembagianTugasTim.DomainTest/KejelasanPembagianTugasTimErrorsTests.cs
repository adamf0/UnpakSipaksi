using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.DomainTest
{
    public class KejelasanPembagianTugasTimErrorsTests
    {
        [Theory]
        [InlineData("KejelasanPembagianTugasTim.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("KejelasanPembagianTugasTim.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("KejelasanPembagianTugasTim.InvalidValueSkor", "Invalid value 'skor'", ErrorType.NotFound)]
        [InlineData("KejelasanPembagianTugasTim.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "KejelasanPembagianTugasTim.EmptyData" => KejelasanPembagianTugasTimErrors.EmptyData(),
                "KejelasanPembagianTugasTim.UnknownKategoriSkema" => KejelasanPembagianTugasTimErrors.UnknownKategoriSkema(),
                "KejelasanPembagianTugasTim.InvalidValueSkor" => KejelasanPembagianTugasTimErrors.InvalidValueSkor(),
                "KejelasanPembagianTugasTim.NotSameValue" => KejelasanPembagianTugasTimErrors.NotSameValue(),
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
            var error = KejelasanPembagianTugasTimErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("KejelasanPembagianTugasTim.NotFound");
            error.Description.Should().Be($"Kejelasan pembagian tugas tim with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
