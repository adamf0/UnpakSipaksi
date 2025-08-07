using FluentAssertions;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas;

namespace UnpakSipaksi.Modules.Insentif.DomainTest
{
    public class InsentifTests
    {
        [Theory]
        [InlineData("12345678900", "Judul", "Jurnal Penerbit", 2, 1, "http://example.com", JenisJurnal.Internal, Peran.FirstAuthor, "2024-01-01", "12", "3", "1-10", "1234-5678", LibatkanMahasiswa.Ya)]
        public void CreateInsentifJurnal_ShouldReturnSuccess(
            string nidn, string judul, string penerbit, int jumlahPenulis,
            int jenisPublikasi, string link, JenisJurnal jenisJurnal,
            Peran peran, string tanggalTerbit, string volume, string edisi,
            string halaman, string doi, LibatkanMahasiswa mahasiswa)
        {
            // Act
            var result = Domain.Insentif.Insentif.CreateInsentifJurnal(
                nidn, judul, penerbit, jumlahPenulis,
                jenisPublikasi, link, jenisJurnal, peran,
                tanggalTerbit, volume, edisi, halaman, doi, mahasiswa
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nidn.Should().Be(nidn);
            result.Value.JudulArtikel.Should().Be(judul);
            result.Value.NamaJurnalPenerbit.Should().Be(penerbit);
            result.Value.JumlahPenulis.Should().Be(jumlahPenulis);
            result.Value.IndexJenisPublikasi.Should().Be(jenisPublikasi);
            result.Value.Link.Should().Be(link);
            result.Value.JenisJurnal.Should().Be((int)jenisJurnal);
            result.Value.Peran.Should().Be(peran.ToString());
            result.Value.Volume.Should().Be(volume);
            result.Value.Edisi.Should().Be(edisi);
            result.Value.Halaman.Should().Be(halaman);
            result.Value.DOI.Should().Be(doi);
            result.Value.LibatkanMahasiswa.Should().Be((int)mahasiswa);
        }

        [Fact]
        public void CreateInsentifJurnal_ShouldRaiseDomainEvent()
        {
            // Arrange
            var result = Domain.Insentif.Insentif.CreateInsentifJurnal(
                "12345678900", "Judul", "Jurnal", 2, 1, "http://link.com",
                JenisJurnal.Internal, Peran.FirstAuthor, "2024-01-01",
                null, null, null, null, LibatkanMahasiswa.Tidak);

            // Assert
            result.Value.DomainEvents.Should().ContainSingle(x => x is InsentifCreatedDomainEvent);
        }

        [Theory]
        [InlineData("", "Judul", "Penerbit", 1, 1, "http://x.com", JenisJurnal.Internal, Peran.FirstAuthor, "2024-01-01", LibatkanMahasiswa.Ya)]
        [InlineData("12345678900", "", "Penerbit", 1, 1, "http://x.com", JenisJurnal.Internal, Peran.FirstAuthor, "2024-01-01", LibatkanMahasiswa.Ya)]
        [InlineData("12345678900", "Judul", "", 1, 1, "http://x.com", JenisJurnal.Internal, Peran.FirstAuthor, "2024-01-01", LibatkanMahasiswa.Ya)]
        [InlineData("12345678900", "Judul", "Penerbit", 0, 1, "http://x.com", JenisJurnal.Internal, Peran.FirstAuthor, "2024-01-01", LibatkanMahasiswa.Ya)]
        [InlineData("12345678900", "Judul", "Penerbit", 1, 1, "", JenisJurnal.Internal, Peran.FirstAuthor, "2024-01-01", LibatkanMahasiswa.Ya)]
        [InlineData("12345678900", "Judul", "Penerbit", 1, 1, "http://x.com", (JenisJurnal)999, Peran.FirstAuthor, "2024-01-01", LibatkanMahasiswa.Ya)]
        //[PR] tamuan test yg tidak dapat di handle
        //[InlineData("12345678900", "Judul", "Penerbit", 1, 1, "http://x.com", JenisJurnal.Internal, (Peran)999, "2024-01-01", LibatkanMahasiswa.Ya)]
        [InlineData("12345678900", "Judul", "Penerbit", 1, 1, "http://x.com", JenisJurnal.Internal, Peran.FirstAuthor, "invalid-date", LibatkanMahasiswa.Ya)]
        public void CreateInsentifJurnal_ShouldReturnFailure_WhenInvalidInput(
            string nidn, string judul, string penerbit, int jumlahPenulis,
            int jenisPublikasi, string link, JenisJurnal jenisJurnal,
            Peran peran, string tanggalTerbit, LibatkanMahasiswa mahasiswa)
        {
            // Act
            var result = Domain.Insentif.Insentif.CreateInsentifJurnal(
                nidn, judul, penerbit, jumlahPenulis, jenisPublikasi,
                link, jenisJurnal, peran, tanggalTerbit,
                null, null, null, null, mahasiswa
            );

            // Assert
            result.IsFailure.Should().BeTrue();
        }
    }
}
