using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori;
using Error = UnpakSipaksi.Common.Domain.Error;

namespace UnpakSipaksi.Modules.KategoriLuaran.DomainTest
{
    public class KategoriLuaranTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            int kategoriId = 1;
            string nama = "ABC";
            string status = "ok";

            // Act
            var result = Domain.KategoriLuaran.KategoriLuaran.Create(
                kategoriId,
                nama,
                status
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.KategoriId.Should().Be(kategoriId);
            result.Value.Nama.Should().Be(nama);
            result.Value.Status.Should().Be(status);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldRaiseKategoriCreatedDomainEvent()
        {
            // Arrange
            int kategoriId = 1;
            string nama = "ABC";
            string status = "ok";

            // Act
            var result = Domain.KategoriLuaran.KategoriLuaran.Create(
                kategoriId,
                nama,
                status
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is KategoriLuaranCreatedDomainEvent);
        }

        [Fact]
        public void Create_WhenInvalidKategoriId_ShouldReturnFailureWithKategoriNotFoundError()
        {
            // Arrange
            int invalidKategoriId = -1;
            string nama = "ABC";
            string status = "ok";

            // Act
            var result = Domain.KategoriLuaran.KategoriLuaran.Create(
                invalidKategoriId,
                nama,
                status
            );

            // Assert
            var expectedError = KategoriLuaranErrors.KategoriNotFound();

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(expectedError.Code);
            result.Error.Description.Should().Be(expectedError.Description);
            result.Error.Type.Should().Be(expectedError.Type);
        }
    }
}
