using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.RAB;

namespace UnpakSipaksi.Modules.PenelitianHibah.DomainTest.RAB
{
    public class RABErrorsTests
    {
        [Fact]
        public void EmptyData_ShouldReturnExpectedError()
        {
            var error = RABErrors.EmptyData();

            error.Code.Should().Be("RAB.EmptyData");
            error.Description.Should().Be("data is not found");
            error.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public void NotFound_ShouldReturnExpectedError()
        {
            var id = Guid.NewGuid();
            var error = RABErrors.NotFound(id);

            error.Code.Should().Be("RAB.NotFound");
            error.Description.Should().Be($"RAB with identifier {id} not found");
        }

        [Fact]
        public void InvalidTotal_ShouldReturnExpectedError()
        {
            var error = RABErrors.InvalidTotal();

            error.Code.Should().Be("RAB.InvalidTotal");
            error.Description.Should().Be("Total value does not match calculation");
        }

        [Fact]
        public void NotFoundHibah_ShouldReturnExpectedError()
        {
            var id = Guid.NewGuid();
            var error = RABErrors.NotFoundHibah(id);

            error.Code.Should().Be("RAB.NotFoundHibah");
            error.Description.Should().Be($"Penelitian hibah with identifier {id} not found");
        }

        [Fact]
        public void InvalidData_ShouldReturnExpectedError()
        {
            var error = RABErrors.InvalidData();

            error.Code.Should().Be("RAB.InvalidData");
            error.Description.Should().Be("Hibah penelitian is not match existing data");
        }
    }
}
