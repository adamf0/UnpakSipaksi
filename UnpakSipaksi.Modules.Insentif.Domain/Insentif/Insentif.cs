using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas;

namespace UnpakSipaksi.Modules.Insentif.Domain.Insentif
{
    public sealed partial class Insentif : Entity
    {
        private Insentif()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }


        [Column("NIDN")]
        public string Nidn { get; private set; }


        [Column("judul_artikel")]
        public string JudulArtikel { get; private set; }


        [Column("nama_jurnal_penerbit")]
        public string NamaJurnalPenerbit { get; private set; }


        [Column("jumlah_penulis")]
        public int JumlahPenulis { get; private set; }


        [Column("id_jenis_publikasi")]
        public int IndexJenisPublikasi { get; private set; } //ref


        [Column("jenis_publikasi")]
        public int JenisPublikasi { get; private set; }


        [Column("link")]
        public string Link { get; private set; }


        [Column("jenis_jurnal")]
        public int JenisJurnal { get; private set; } //0, 1


        [Column("peran")]
        public string Peran { get; private set; }


        [Column("tanggal_terbit")]
        public DateTime TanggalTerbit { get; private set; }


        [Column("volume")]
        public string? Volume { get; private set; }


        [Column("edisi")]
        public string? Edisi { get; private set; }


        [Column("halaman")]
        public string? Halaman { get; private set; }


        [Column("DOI")]
        public string? DOI { get; private set; }


        [Column("nama_kegiatan_seminar")]
        public string? NamaKegiatanSeminar { get; private set; }


        [Column("mahasiswa")]
        public int LibatkanMahasiswa { get; private set; }


        private static int[] jenisJurnalTerdaftar  = EnumExtensions.GetAllEnumValues<JenisJurnal>();
        private static string[] peranTerdaftar     = EnumExtensions.GetAllEnumStrings<Peran>();

        public static Result<Insentif> UpdateInsentifJurnal(
            Insentif prev,
            string Nidn,
            string JudulArtikel,
            string NamaJurnalPenerbit,
            int JumlahPenulis,
            int IndexJenisPublikasi,
            string Link,
            JenisJurnal JenisJurnal, //1: internal 0:external
            Peran Peran, //First Author, Co Author
            string TanggalTerbit,
            string? Volume,
            string? Edisi,
            string? Halaman,
            string? DOI,
            LibatkanMahasiswa LibatkanMahasiswa //1: Ya 0: Tidak
        )
        {
            var validTanggal = DateTime.TryParseExact(TanggalTerbit, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTanggal);

            if (string.IsNullOrWhiteSpace(Nidn))
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyNidn());
            }
            else if (!DomainValidator.IsValidNidn(Nidn))
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidNidn());
            }
            else if (string.IsNullOrWhiteSpace(JudulArtikel))
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyJudulArtikel());
            }
            else if (string.IsNullOrWhiteSpace(NamaJurnalPenerbit))
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyNamaJurnalPenerbit());
            }
            else if (JumlahPenulis < 1)
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidJumlahPenulis());
            }
            else if (string.IsNullOrWhiteSpace(Link))
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidLink());
            }
            else if (
                !jenisJurnalTerdaftar.Contains(Convert.ToInt32(JenisJurnal))
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidJenisJurnal());
            }
            else if (
                !peranTerdaftar.Contains(Peran.ToEnumString())
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidPeran());
            }
            else if (
                string.IsNullOrWhiteSpace(TanggalTerbit) ||
                !validTanggal
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidTanggalTerbit());
            }
            /*else if (
                !EnumExtensions.GetAllEnumValues<JenisPublikasi>().Contains(Convert.ToInt32(JenisPublikasi))
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidJenisPublikasi());
            }
            else if (
                EnumExtensions.GetAllEnumValues<JenisPublikasi>().Contains(Convert.ToInt32(JenisPublikasi)) &&
                JenisPublikasi== JenisPublikasi.Prosiding && 
                string.IsNullOrEmpty(NamaKegiatanSeminar)
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyNamaKegiatanSeminar());
            }*/

            prev.Nidn = Nidn;
            prev.JudulArtikel = JudulArtikel;
            prev.NamaJurnalPenerbit = NamaJurnalPenerbit;
            prev.JumlahPenulis = JumlahPenulis;
            prev.IndexJenisPublikasi = IndexJenisPublikasi;
            prev.JenisPublikasi = (int)Domain.Insentif.JenisPublikasi.Jurnal;
            prev.Link = Link;
            prev.JenisJurnal = (int) JenisJurnal; //1: internal 0:external
            prev.Peran = Peran.ToString(); //First Author, Co Author
            prev.TanggalTerbit = parsedTanggal;
            prev.Volume = Volume;
            prev.Edisi = Edisi;
            prev.Halaman = Halaman;
            prev.DOI = DOI;
            prev.LibatkanMahasiswa = (int)LibatkanMahasiswa;

            return prev;
        }

        public static Result<Insentif> UpdateInsentifProsiding(
            Insentif prev,
            string Nidn,
            string JudulArtikel,
            string NamaJurnalPenerbit,
            int JumlahPenulis,
            int IndexJenisPublikasi,
            string Link,
            JenisJurnal JenisJurnal, //1: internal 0:external
            Peran Peran, //First Author, Co Author
            string TanggalTerbit,
            string NamaKegiatanSeminar,
            LibatkanMahasiswa LibatkanMahasiswa //1: Ya 0: Tidak
        )
        {
            var validTanggal = DateTime.TryParseExact(TanggalTerbit, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTanggal);

            if (string.IsNullOrWhiteSpace(Nidn))
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyNidn());
            }
            else if (!DomainValidator.IsValidNidn(Nidn))
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidNidn());
            }
            else if (string.IsNullOrWhiteSpace(JudulArtikel))
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyJudulArtikel());
            }
            else if (string.IsNullOrWhiteSpace(NamaJurnalPenerbit))
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyNamaJurnalPenerbit());
            }
            else if (JumlahPenulis < 1)
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidJumlahPenulis());
            }
            else if (string.IsNullOrWhiteSpace(Link))
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidLink());
            }
            else if (
                !jenisJurnalTerdaftar.Contains(Convert.ToInt32(JenisJurnal))
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidJenisJurnal());
            }
            else if (
                !peranTerdaftar.Contains(Peran.ToEnumString())
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidPeran());
            }
            else if (
                string.IsNullOrWhiteSpace(TanggalTerbit) ||
                !validTanggal
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidTanggalTerbit());
            }
            /*else if (
                !EnumExtensions.GetAllEnumValues<JenisPublikasi>().Contains(Convert.ToInt32(JenisPublikasi))
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidJenisPublikasi());
            }
            else if (
                EnumExtensions.GetAllEnumValues<JenisPublikasi>().Contains(Convert.ToInt32(JenisPublikasi)) &&
                JenisPublikasi == JenisPublikasi.Prosiding &&
                string.IsNullOrEmpty(NamaKegiatanSeminar)
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyNamaKegiatanSeminar());
            }*/

            prev.Nidn = Nidn;
            prev.JudulArtikel = JudulArtikel;
            prev.NamaJurnalPenerbit = NamaJurnalPenerbit;
            prev.JumlahPenulis = JumlahPenulis;
            prev.IndexJenisPublikasi = IndexJenisPublikasi;
            prev.JenisPublikasi = (int)Domain.Insentif.JenisPublikasi.Prosiding;
            prev.Link = Link;
            prev.JenisJurnal = (int)JenisJurnal; //1: internal 0:external
            prev.Peran = Peran.ToString(); //First Author, Co Author
            prev.TanggalTerbit = parsedTanggal;
            prev.NamaKegiatanSeminar = NamaKegiatanSeminar;
            prev.LibatkanMahasiswa = (int) LibatkanMahasiswa;

            return prev;
        }


        public static Result<Insentif> CreateInsentifJurnal(
            string Nidn,
            string JudulArtikel,
            string NamaJurnalPenerbit,
            int JumlahPenulis,
            int IndexJenisPublikasi,
            string Link,
            JenisJurnal JenisJurnal, //1: internal 0:external
            Peran Peran, //First Author, Co Author
            string TanggalTerbit,
            string? Volume,
            string? Edisi,
            string? Halaman,
            string? DOI,
            LibatkanMahasiswa LibatkanMahasiswa //1: Ya 0: Tidak
        )
        {
            var validTanggal = DateTime.TryParseExact(TanggalTerbit, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTanggal);

            if (string.IsNullOrWhiteSpace(Nidn))
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyNidn());
            }
            else if (!DomainValidator.IsValidNidn(Nidn))
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidNidn());
            }
            else if (string.IsNullOrWhiteSpace(JudulArtikel))
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyJudulArtikel());
            }
            else if (string.IsNullOrWhiteSpace(NamaJurnalPenerbit))
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyNamaJurnalPenerbit());
            }
            else if (JumlahPenulis<1)
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidJumlahPenulis());
            }
            else if (string.IsNullOrWhiteSpace(Link))
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidLink());
            }
            else if (
                !jenisJurnalTerdaftar.Contains(Convert.ToInt32(JenisJurnal))
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidJenisJurnal());
            }
            else if (
                !peranTerdaftar.Contains(Peran.ToEnumString())
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidPeran());
            }
            else if (
                string.IsNullOrWhiteSpace(TanggalTerbit) ||
                !validTanggal
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidTanggalTerbit());
            }
            /*else if (
                !EnumExtensions.GetAllEnumValues<JenisPublikasi>().Contains(Convert.ToInt32(JenisPublikasi))
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidJenisPublikasi());
            }
            else if (
                EnumExtensions.GetAllEnumValues<JenisPublikasi>().Contains(Convert.ToInt32(JenisPublikasi)) &&
                JenisPublikasi== JenisPublikasi.Prosiding && 
                string.IsNullOrEmpty(NamaKegiatanSeminar)
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyNamaKegiatanSeminar());
            }*/

            var asset = new Insentif
            {
                Uuid = Guid.NewGuid(),
                Nidn = Nidn,
                JudulArtikel = JudulArtikel,
                NamaJurnalPenerbit = NamaJurnalPenerbit,
                JumlahPenulis = JumlahPenulis,
                IndexJenisPublikasi = IndexJenisPublikasi,
                JenisPublikasi = (int)Domain.Insentif.JenisPublikasi.Jurnal,
                Link = Link,
                JenisJurnal = (int)JenisJurnal, //1: internal 0:external
                Peran = Peran.ToString(), //First Author, Co Author
                TanggalTerbit = parsedTanggal,
                Volume = Volume,
                Edisi = Edisi,
                Halaman = Halaman,
                DOI = DOI,
                LibatkanMahasiswa = (int)LibatkanMahasiswa,
            };

            asset.Raise(new InsentifCreatedDomainEvent(asset.Uuid));

            return asset;
        }

        public static Result<Insentif> CreateInsentifProsiding(
            string Nidn,
            string JudulArtikel,
            string NamaJurnalPenerbit,
            int JumlahPenulis,
            int IndexJenisPublikasi,
            string Link,
            JenisJurnal JenisJurnal, //1: internal 0:external
            Peran Peran, //First Author, Co Author
            string TanggalTerbit,
            string NamaKegiatanSeminar,
            LibatkanMahasiswa LibatkanMahasiswa //1: Ya 0: Tidak
        )
        {
            var validTanggal = DateTime.TryParseExact(TanggalTerbit, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTanggal);

            if (string.IsNullOrWhiteSpace(Nidn))
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyNidn());
            }
            else if (!DomainValidator.IsValidNidn(Nidn))
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidNidn());
            }
            else if (string.IsNullOrWhiteSpace(JudulArtikel))
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyJudulArtikel());
            }
            else if (string.IsNullOrWhiteSpace(NamaJurnalPenerbit))
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyNamaJurnalPenerbit());
            }
            else if (JumlahPenulis < 1)
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidJumlahPenulis());
            }
            else if (string.IsNullOrWhiteSpace(Link))
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidLink());
            }
            else if (
                !jenisJurnalTerdaftar.Contains(Convert.ToInt32(JenisJurnal))
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidJenisJurnal());
            }
            else if (
                !peranTerdaftar.Contains(Peran.ToEnumString())
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidPeran());
            }
            else if (
                string.IsNullOrWhiteSpace(TanggalTerbit) ||
                !validTanggal
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidTanggalTerbit());
            }
            /*else if (
                !EnumExtensions.GetAllEnumValues<JenisPublikasi>().Contains(Convert.ToInt32(JenisPublikasi))
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.InvalidJenisPublikasi());
            }
            else if (
                EnumExtensions.GetAllEnumValues<JenisPublikasi>().Contains(Convert.ToInt32(JenisPublikasi)) &&
                JenisPublikasi == JenisPublikasi.Prosiding &&
                string.IsNullOrEmpty(NamaKegiatanSeminar)
            )
            {
                return Result.Failure<Insentif>(InsentifErrors.EmptyNamaKegiatanSeminar());
            }*/

            var asset = new Insentif
            {
                Uuid = Guid.NewGuid(),
                Nidn = Nidn,
                JudulArtikel = JudulArtikel,
                NamaJurnalPenerbit = NamaJurnalPenerbit,
                JumlahPenulis = JumlahPenulis,
                IndexJenisPublikasi = IndexJenisPublikasi,
                JenisPublikasi = (int)Domain.Insentif.JenisPublikasi.Jurnal,
                Link = Link,
                JenisJurnal = (int)JenisJurnal, //1: internal 0:external
                Peran = Peran.ToString(), //First Author, Co Author
                TanggalTerbit = parsedTanggal,
                NamaKegiatanSeminar = NamaKegiatanSeminar,
                LibatkanMahasiswa = (int)LibatkanMahasiswa
            };

            asset.Raise(new InsentifCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
