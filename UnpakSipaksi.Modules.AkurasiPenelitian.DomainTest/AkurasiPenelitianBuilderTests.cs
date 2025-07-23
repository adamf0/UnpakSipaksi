using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using Xunit;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.DomainTest
{
    public class AkurasiPenelitianBuilderTests
    {
        [InlineData("AkurasiPenelitian ABC", 1)]
        [InlineData("AkurasiPenelitian A", 2)]
        public void Change_ShouldUpdateSuccessfully(string expectedNama, int expectedSkor)
        {
            // Arrange
            string nama = "AkurasiPenelitian A";
            int skor = 1;

            var komponen = Domain.AkurasiPenelitian.AkurasiPenelitian.Create(
                nama,
                skor
            );
            var builder = new Domain.AkurasiPenelitian.AkurasiPenelitian.AkurasiPenelitianBuilder(komponen.Value);

            // Act
            builder.ChangeNama(expectedNama).ChangeSkor(expectedSkor);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be("Updated AkurasiPenelitian");
            result.Value.Skor.Should().Be(100);
        }

        [Theory]
        [InlineData("Updated AkurasiPenelitian", -1, ErrorType.NotFound)]
        //[InlineData("Updated AkurasiPenelitian", 2147483648, ErrorType.NotFound)]
        public void Build_ShouldReturnFailure_WhenInvalidChangeOccurs(string expectedNama, int expectedSkor, ErrorType expectedType)
        {
            // Arrange
            string nama = "AkurasiPenelitian A";
            int skor = 1000;

            var komponen = Domain.AkurasiPenelitian.AkurasiPenelitian.Create(
                nama,
                skor
            );
            var builder = new Domain.AkurasiPenelitian.AkurasiPenelitian.AkurasiPenelitianBuilder(komponen.Value);

            // Act
            // Simulating a failure scenario (you could add conditions inside builder that trigger failure)
            builder.ChangeNama(expectedNama).ChangeSkor(expectedSkor);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();  // Expect failure
            result.Error.Code.Should().Be("AkurasiPenelitian.InvalidSkor");  // Expect specific error code
            result.Error.Description.Should().Be("Skor is invalid format");  // Expect specific error message
        }
    }
}
