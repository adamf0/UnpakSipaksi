using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Insentif.Domain.CheckInsentif;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiLppm;

namespace UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas
{
    public sealed partial class VerifikasiFakultas : Entity
    {
        private VerifikasiFakultas()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }
        [Column("bukti_publikasi")]
        public int BuktiPublikasi { get; private set; }
        [Column("status_jurnal", TypeName = "VARCHAR(36)")]
        public int StatusJurnal { get; private set; }
        [Column("peran_penulis")]
        public string PeranPenulis { get; private set; }
        [Column("cek_jumlah_penulis")]
        public int JumlahPenulis { get; private set; }
        [Column("jurnal_internal")]
        public int JenisJurnal { get; private set; }
        [Column("mahasiswa_c")]
        public int LibatkanMahasiswa { get; private set; }
        [Column("sbu")]
        public int SBU { get; private set; }
        [Column("porsi_sbu")]
        public int Porsi { get; private set; }
        [Column("cek_insentif")]
        public decimal Insentif { get; private set; }
        [Column("status_pengajuan")]
        public int StatusPengajuan { get; private set; }
        [Column("keterangan")]
        public string? Catatan { get; private set; }

        public static Result<VerifikasiFakultas> Review(
            Guid Uuid,
            Domain.VerifikasiFakultas.VerifikasiFakultas? prev,
            BuktiPublikasi BuktiPublikasi,
            int StatusJurnal,
            Peran PeranPenulis,
            int JumlahPenulis,
            JenisJurnal JenisJurnal,
            LibatkanMahasiswa LibatkanMahasiswa,
            int SBU,
            StatusPengajuan StatusPengajuan,
            string? Catatan
        )
        {
            if (prev == null)
            {
                return Result.Failure<VerifikasiFakultas>(VerifikasiFakultasErrors.NotFound(Uuid));
            }
            if (
                !EnumExtensions.GetAllEnumValues<BuktiPublikasi>().Contains(Convert.ToInt32(BuktiPublikasi))
            )
            {
                return Result.Failure<VerifikasiFakultas>(VerifikasiFakultasErrors.InvalidBuktiPublikasi());
            }
            if (StatusJurnal<1)
            {
                return Result.Failure<VerifikasiFakultas>(VerifikasiFakultasErrors.InvalidStatusJurnal());
            }
            if (
                !EnumExtensions.GetAllEnumValues<Peran>().Contains(Convert.ToInt32(PeranPenulis))
            )
            {
                return Result.Failure<VerifikasiFakultas>(VerifikasiFakultasErrors.InvalidPeranPenulis());
            }
            if (JumlahPenulis < 1)
            {
                return Result.Failure<VerifikasiFakultas>(VerifikasiFakultasErrors.InvalidJumlahPenulis());
            }
            if (
                !EnumExtensions.GetAllEnumValues<JenisJurnal>().Contains(Convert.ToInt32(JenisJurnal))
            )
            {
                return Result.Failure<VerifikasiFakultas>(VerifikasiFakultasErrors.InvalidJenisJurnal());
            }
            if (
                !EnumExtensions.GetAllEnumValues<LibatkanMahasiswa>().Contains(Convert.ToInt32(LibatkanMahasiswa))
            )
            {
                return Result.Failure<VerifikasiFakultas>(VerifikasiFakultasErrors.InvalidLibatkanMahasiswa());
            }
            if (SBU < 1)
            {
                return Result.Failure<VerifikasiFakultas>(VerifikasiFakultasErrors.InvalidSBU());
            }

            if (
                !EnumExtensions.GetAllEnumValues<StatusPengajuan>().Contains(Convert.ToInt32(StatusPengajuan))
            )
            {
                return Result.Failure<VerifikasiFakultas>(VerifikasiFakultasErrors.InvalidStatusPengajuan());
            }
            if (
                EnumExtensions.GetAllEnumValues<StatusPengajuan>().Contains(Convert.ToInt32(StatusPengajuan)) &&
                StatusPengajuan == StatusPengajuan.Tidak && 
                string.IsNullOrEmpty(Catatan)
            )
            {
                return Result.Failure<VerifikasiFakultas>(VerifikasiFakultasErrors.EmptyCatatan());
            }

            var input = new InsentifInput
            {
                JenisJurnal = JenisJurnal,
                Mahasiswa = LibatkanMahasiswa == LibatkanMahasiswa.Ya ? true : false,
                PeranPenulis = PeranPenulis,
                SBU = SBU,
                JumlahCoAuthor = JumlahPenulis
            };
            var calculator = InsentifCalculator.Hitung(input);

            if (calculator.IsFailure) 
                return Result.Failure<VerifikasiFakultas>(calculator.Error);

            if (calculator.Value.PorsiSBU < 1)
                return Result.Failure<VerifikasiFakultas>(VerifikasiFakultasErrors.InvalidPorsi());

            if (calculator.Value.TotalInsentif <= 0)
                return Result.Failure<VerifikasiFakultas>(VerifikasiFakultasErrors.InvalidCalculateMoney());

            var asset = new VerifikasiFakultas
            {
                Id = prev.Id,
                Uuid = prev.Uuid,
                BuktiPublikasi = (int) BuktiPublikasi,
                StatusJurnal = StatusJurnal,
                PeranPenulis = PeranPenulis.ToString(),
                JumlahPenulis = JumlahPenulis,
                JenisJurnal = (int) JenisJurnal,
                LibatkanMahasiswa = (int) LibatkanMahasiswa,
                SBU = SBU,
                Porsi = calculator.Value.PorsiSBU,
                Insentif = calculator.Value.TotalInsentif,
                StatusPengajuan = (int) StatusPengajuan,
                Catatan = Catatan
            };

            asset.Raise(new VerifikasiFakultasCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
