using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.DomainTest.MemberDosen
{
    public class MemberDosenErrorsTests
    {
        [Theory]
        [InlineData("MemberDosen.EmptyData", "data is not found", ErrorType.NotFound)]
        [InlineData("MemberDosen.InvalidNidn", "Nidn is invalid format", ErrorType.NotFound)]
        public void StaticErrors_ShouldReturnCorrectValues(string expectedCode, string expectedDescription, ErrorType expectedType)
        {
            // Act
            Error error = expectedCode switch
            {
                "MemberDosen.EmptyData" => MemberDosenErrors.EmptyData(),
                "MemberDosen.InvalidNidn" => MemberDosenErrors.InvalidNidn(),
                _ => throw new ArgumentException("Unknown error code", nameof(expectedCode))
            };

            // Assert
            error.Code.Should().Be(expectedCode);
            error.Description.Should().Be(expectedDescription);
            error.Type.Should().Be(expectedType);
        }

        public static IEnumerable<object[]> NotFoundTestData =>
            new List<object[]>
            {
                new object[] { Guid.Parse("11111111-1111-1111-1111-111111111111"), 1, ErrorType.NotFound },
                new object[] { Guid.Parse("22222222-2222-2222-2222-222222222222"), 2, ErrorType.NotFound }
            };

        [Theory]
        [MemberData(nameof(NotFoundTestData))]
        public void NotFound_ShouldReturnCorrectValues(Guid id, int type, ErrorType expectedType)
        {
            // Act
            var error = type == 1
                ? MemberDosenErrors.NotFound(id)
                : MemberDosenErrors.NotFoundHibah(id);

            // Assert
            var expectedCode = type == 1 ? "MemberDosen.NotFound" : "MemberDosen.NotFoundHibah";
            var expectedDesc = type == 1
                ? $"Member hibah dosen with identifier {id} not found"
                : $"Penelitian hibah with identifier {id} not found";

            error.Code.Should().Be(expectedCode);
            error.Description.Should().Be(expectedDesc);
            error.Type.Should().Be(expectedType);
        }

        [Theory]
        [InlineData("065117251", ErrorType.NotFound)]
        public void NotUnique_ShouldReturnCorrectValues(string nidn, ErrorType expectedType)
        {
            // Act
            var error = MemberDosenErrors.NotUnique(nidn);

            // Assert
            error.Code.Should().Be("MemberDosen.NotUnique");
            error.Description.Should().Be($"Nidn {nidn} is not unique");
            error.Type.Should().Be(expectedType);
        }
    }
}
