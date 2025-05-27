using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Komponen.Domain.Komponen;
using Xunit;

namespace UnpakSipaksi.Modules.Komponen.DomainTest
{
    public class KomponenBuilderTests
    {
        [Fact]
        public void ChangeNama_ShouldUpdateNamaSuccessfully()
        {
            // Arrange
            string nama = "Komponen A";
            int? maxBiaya = 1000;

            var komponen = Domain.Komponen.Komponen.Create(
                nama,
                maxBiaya
            );
            var builder = new Domain.Komponen.Komponen.KomponenBuilder(komponen.Value);

            // Act
            builder.ChangeNama("Updated Komponen");
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be("Updated Komponen");
        }

        [Fact]
        public void ChangeMaxBiaya_ShouldUpdateMaxBiayaSuccessfully()
        {
            // Arrange
            string nama = "Komponen A";
            int? maxBiaya = 1000;

            var komponen = Domain.Komponen.Komponen.Create(
                nama,
                maxBiaya
            );
            var builder = new Domain.Komponen.Komponen.KomponenBuilder(komponen.Value);

            // Act
            builder.ChangeMaxBiaya(2000);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.MaxBiaya.Should().Be(2000);
        }

        [Fact]
        public void Build_ShouldReturnFailure_WhenInvalidChangeOccurs()
        {
            // Arrange
            string nama = "Komponen A";
            int? maxBiaya = 1000;

            var komponen = Domain.Komponen.Komponen.Create(
                nama,
                maxBiaya
            );
            var builder = new Domain.Komponen.Komponen.KomponenBuilder(komponen.Value);

            // Act
            // Simulating a failure scenario (you could add conditions inside builder that trigger failure)
            builder.ChangeNama(string.Empty);  // Simulating invalid nama change
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();  // Expect failure
            result.Error.Code.Should().Be("Komponen.NamaEmpty");  // Expect specific error code
            result.Error.Description.Should().Be("Nama can't be empty");  // Expect specific error message
        }
    }
}
