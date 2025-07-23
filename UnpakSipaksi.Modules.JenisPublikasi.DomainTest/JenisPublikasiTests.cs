using FluentAssertions;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi;

namespace UnpakSipaksi.Modules.JenisPublikasi.DomainTest
{
    public class JenisPublikasiTests
    {
        [Fact]
        public void Create_ShouldReturnJenisPublikasiWithCorrectProperties()
        {
            // Arrange
            string nama = "ABC";
            int sbu = 100;

            // Act
            var result = Domain.JenisPublikasi.JenisPublikasi.Create(
                nama,
                sbu
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(nama);
            result.Value.Sbu.Should().Be(sbu);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_ShouldRaiseJenisPublikasiCreatedDomainEvent()
        {
            // Arrange
            string nama = "ABC";
            int sbu = 100;

            // Act
            var result = Domain.JenisPublikasi.JenisPublikasi.Create(
                nama,
                sbu
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is JenisPublikasiCreatedDomainEvent);
        }

        [Fact]
        public void Create_ShouldInvalidWhenIncorrectData()
        {
            // Arrange
            string nama = "ABC";
            int sbu = -100;

            // Act
            var result = Domain.JenisPublikasi.JenisPublikasi.Create(
                nama,
                sbu
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("JenisPublikasi.InvalidSbu");
            result.Error.Description.Should().Be("Sbu invalid format");
        }
    }
}