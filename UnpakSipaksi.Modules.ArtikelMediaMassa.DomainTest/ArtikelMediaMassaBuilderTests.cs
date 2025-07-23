using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using Xunit;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.DomainTest
{
    public class ArtikelMediaMassaBuilderTests
    {
        [Fact]
        public void ChangeNama_ShouldUpdateSuccessfully()
        {
            // Arrange
            string nama = "ArtikelMediaMassa A";
            int nilai = 1;

            var komponen = Domain.ArtikelMediaMassa.ArtikelMediaMassa.Create(
                nama,
                nilai
            );
            var builder = new Domain.ArtikelMediaMassa.ArtikelMediaMassa.ArtikelMediaMassaBuilder(komponen.Value);

            // Act
            builder.ChangeNama("Updated ArtikelMediaMassa").ChangeNilai(100);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be("Updated ArtikelMediaMassa");
            result.Value.Nilai.Should().Be(100);
        }

        [Theory]
        [InlineData("Updated ArtikelMediaMassa", -1, ErrorType.NotFound)]
        //[InlineData("Updated ArtikelMediaMassa", 2147483648, ErrorType.NotFound)]
        public void Build_ShouldReturnFailure_WhenInvalidChangeOccurs(string expectedNama, int expectedNilai, ErrorType expectedType)
        {
            // Arrange
            string nama = "ArtikelMediaMassa A";
            int nilai = 1000;

            var komponen = Domain.ArtikelMediaMassa.ArtikelMediaMassa.Create(
                nama,
                nilai
            );
            var builder = new Domain.ArtikelMediaMassa.ArtikelMediaMassa.ArtikelMediaMassaBuilder(komponen.Value);

            // Act
            // Simulating a failure scenario (you could add conditions inside builder that trigger failure)
            builder.ChangeNama(expectedNama).ChangeNilai(expectedNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();  // Expect failure
            result.Error.Code.Should().Be("ArtikelMediaMassa.InvalidNilai");  // Expect specific error code
            result.Error.Description.Should().Be("Nilai is invalid format");  // Expect specific error message
        }
    }
}
