using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Komponen.Domain.Komponen;
using Xunit;

namespace UnpakSipaksi.Modules.Komponen.DomainTest
{
    public class KomponenErrorsTests
    {
        [Fact]
        public void EmptyData_ShouldReturnCorrectError()
        {
            // Act
            var error = KomponenErrors.EmptyData();

            // Assert
            error.Code.Should().Be("Komponen.EmptyData");
            error.Description.Should().Be("data is not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public void NotFound_ShouldReturnCorrectError()
        {
            // Arrange
            var komponenId = Guid.NewGuid();

            // Act
            var error = KomponenErrors.NotFound(komponenId);

            // Assert
            error.Code.Should().Be("Komponen.NotFound");
            error.Description.Should().Be($"Komponen with the identifier {komponenId} was not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public void NamaEmpty_ShouldReturnCorrectError()
        {
            // Act
            var error = KomponenErrors.NamaEmpty();

            // Assert
            error.Code.Should().Be("Komponen.NamaEmpty");
            error.Description.Should().Be("Nama can't be empty");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
