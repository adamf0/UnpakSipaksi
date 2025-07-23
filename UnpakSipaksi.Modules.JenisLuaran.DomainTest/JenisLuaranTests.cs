using FluentAssertions;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisLuaran.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UnpakSipaksi.Modules.JenisLuaran.DomainTest
{
    public class JenisLuaranTests
    {
        [Fact]
        public void Create_ShouldReturnJenisLuaranWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.JenisLuaran.Create(
                nama
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(nama);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_ShouldRaiseJenisLuaranCreatedDomainEvent()
        {
            // Arrange
            string nama = "ABC";

            // Act
            var result = Domain.JenisLuaran.Create(
                nama
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is JenisLuaranCreatedDomainEvent);
        }

        [Fact]
        public void Update_ShouldReturnUpdatedJenisLuaran()
        {
            // Arrange
            string nama = "ABC";

            var createResult = Domain.JenisLuaran.Create(nama);
            var jenisLuaran = createResult.Value;

            // Act
            string namaChange = "AAA";
            var updatedJenisLuaran = Domain.JenisLuaran.Update(
                jenisLuaran.Uuid,
                namaChange,
                jenisLuaran
            );

            // Assert
            updatedJenisLuaran.IsSuccess.Should().BeTrue();
            updatedJenisLuaran.Value.Nama.Should().Be(namaChange);
            updatedJenisLuaran.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Update_ShouldReturnInvalid()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            string namaChange = "AAA";
            var updatedJenisLuaran = Domain.JenisLuaran.Update(
                id,
                namaChange,
                null
            );

            // Assert
            updatedJenisLuaran.IsFailure.Should().BeTrue();
            updatedJenisLuaran.IsFailure.Should().BeTrue();
            updatedJenisLuaran.Error.Code.Should().Be("JenisLuaran.NotFound");
            updatedJenisLuaran.Error.Description.Should().Be($"Jenis luaran with identifier {id} not found");
            updatedJenisLuaran.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}