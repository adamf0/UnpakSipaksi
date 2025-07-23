using FluentAssertions;
using UnpakSipaksi.Modules.Referensi.Domain.Referensi;

namespace UnpakSipaksi.Modules.Referensi.DomainTest
{
    public class ReferensiTests
    {
        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldReturnKategoriWithCorrectProperties()
        {
            // Arrange
            string Nama = "abc";
            int KebaruanReferensiId = 1;
            int RelevansiKualitasReferensiId = 2;
            int Nilai = 10;

            // Act
            var result = Domain.Referensi.Referensi.Create(
                Nama,
                KebaruanReferensiId,
                RelevansiKualitasReferensiId,
                Nilai
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(Nama);
            result.Value.KebaruanReferensiId.Should().Be(KebaruanReferensiId);
            result.Value.RelevansiKualitasReferensiId.Should().Be(RelevansiKualitasReferensiId);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_WhenValidPropertiesProvided_ShouldRaiseKategoriCreatedDomainEvent()
        {
            // Arrange
            string Nama = "abc";
            int KebaruanReferensiId = 1;
            int RelevansiKualitasReferensiId = 2;
            int Nilai = 10;

            // Act
            var result = Domain.Referensi.Referensi.Create(
                Nama,
                KebaruanReferensiId,
                RelevansiKualitasReferensiId,
                Nilai
            );

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is ReferensiCreatedDomainEvent);
        }
    }
}
