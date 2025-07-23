using FluentAssertions;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.IndikatorCapaian.Domain;

namespace UnpakSipaksi.Modules.IndikatorCapaian.DomainTest
{
    public class IndikatorCapaianTests
    {
        [Theory]
        [InlineData(1, "A", null)]
        [InlineData(1, "A", "B")]
        public void Create_ShouldReturnIndikatorCapaianWithCorrectProperties(int JenisLuaranId, string Nama, string? Status)
        {
            // Arrange

            // Act
            var result = Domain.IndikatorCapaian.Create(
                JenisLuaranId,
                Nama,
                Status
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.JenisLuaranId.Should().Be(JenisLuaranId);
            result.Value.Nama.Should().Be(Nama);
            result.Value.Status.Should().Be(Status);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_ShouldRaiseIndikatorCapaianCreatedDomainEvent()
        {
            // Arrange
            int JenisLuaranId = 1;
            string Nama = "A";
            string Status = null;

            // Act
            var result = Domain.IndikatorCapaian.Create(
                JenisLuaranId,
                Nama,
                Status
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is IndikatorCapaianCreatedDomainEvent);
        }

        [Theory]
        [InlineData(1, "A", null)]
        [InlineData(1, "A", "B")]
        public void Update_ShouldReturnIndikatorCapaianWithCorrectProperties(int JenisLuaranId, string Nama, string? Status)
        {
            // Arrange
            Result<Domain.IndikatorCapaian> prev = Domain.IndikatorCapaian.Create(1, "AA", null);

            // Act
            var result = Domain.IndikatorCapaian.Update(
                prev.Value.Uuid,
                JenisLuaranId,
                Nama,
                Status,
                prev.Value
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.JenisLuaranId.Should().Be(JenisLuaranId);
            result.Value.Nama.Should().Be(Nama);
            result.Value.Status.Should().Be(Status);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Theory]
        [InlineData("IndikatorCapaian.UnknownJenisLuaran", "Unknown jenis luaran", ErrorType.NotFound, -1, "A", null, true)]
        [InlineData("IndikatorCapaian.NotFound", "Indikator capaian with identifier ? not found", ErrorType.NotFound, 1, "A", "B", false)]
        public void Update_ShouldReturnInvalid(string expectedCode, string expectedDescription, ErrorType expectedType, int JenisLuaranId, string Nama, string? Status, bool modeValidPrev)
        {
            // Arrange
            Result<Domain.IndikatorCapaian>? prev =  modeValidPrev? Domain.IndikatorCapaian.Create(1, "AA", null):null;

            Guid id = prev?.Value?.Uuid ?? Guid.NewGuid();
            Error error = expectedCode switch
            {
                "IndikatorCapaian.UnknownJenisLuaran" => IndikatorCapaianErrors.UnknownJenisLuaran(),
                "IndikatorCapaian.NotFound" => IndikatorCapaianErrors.NotFound(id),
                _ => throw new ArgumentException("Unknown error code", nameof(expectedCode))
            };

            // Act
            var result = Domain.IndikatorCapaian.Update(
                id,
                JenisLuaranId,
                Nama,
                Status,
                prev?.Value ?? null
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            error.Code.Should().Be(expectedCode);
            error.Description.Should().Be(expectedDescription.Replace("?", id.ToString()));
            error.Type.Should().Be(expectedType);
        }
    }
}