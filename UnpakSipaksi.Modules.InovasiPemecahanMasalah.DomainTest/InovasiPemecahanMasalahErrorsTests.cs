using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah;
using Xunit;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.DomainTest
{
    public class InovasiPemecahanMasalahErrorsTests
    {
        [Theory]
        [InlineData("InovasiPemecahanMasalah.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("InovasiPemecahanMasalah.NotSameValue", "not the same value in data 'nilai'", ErrorType.NotFound)]
        [InlineData("InovasiPemecahanMasalah.UnknownKategoriSkema", "Unknown schema category", ErrorType.NotFound)]
        [InlineData("InovasiPemecahanMasalah.InvalidSkor", "Skor is invalid format", ErrorType.NotFound)]
        public void StaticError_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "InovasiPemecahanMasalah.EmptyData" => InovasiPemecahanMasalahErrors.EmptyData(),
                "InovasiPemecahanMasalah.NotSameValue" => InovasiPemecahanMasalahErrors.NotSameValue(),
                "InovasiPemecahanMasalah.UnknownKategoriSkema" => InovasiPemecahanMasalahErrors.UnknownKategoriSkema(),
                "InovasiPemecahanMasalah.InvalidSkor" => InovasiPemecahanMasalahErrors.InvalidSkor(),
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
            var error = InovasiPemecahanMasalahErrors.NotFound(id);

            // Assert
            error.Code.Should().Be("InovasiPemecahanMasalah.NotFound");
            error.Description.Should().Be($"Inovasi pemecahan masalah with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
