using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberNonDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.DomainTest.MemberNonDosen
{
    public class MemberNonDosenErrorsTests
    {
        [Theory]
        [InlineData("MemberNonDosen.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("MemberNonDosen.NotFoundHibah", "Penelitian hibah not found", ErrorType.NotFound)]
        public void StaticErrors_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "MemberNonDosen.EmptyData" => MemberNonDosenErrors.EmptyData(),
                "MemberNonDosen.NotFoundHibah" => MemberNonDosenErrors.NotFoundHibah(),
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
            var id = Guid.NewGuid();
            var error = MemberNonDosenErrors.NotFound(id);

            error.Code.Should().Be("MemberNonDosen.NotFound");
            error.Description.Should().Be($"Member hibah dosen with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public void NotFoundHibah_ShouldReturnCorrectError()
        {
            var id = Guid.NewGuid();
            var error = MemberNonDosenErrors.NotFoundHibah(id);

            error.Code.Should().Be("MemberNonDosen.NotFoundHibah");
            error.Description.Should().Be($"Penelitian hibah with identifier {id} not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
