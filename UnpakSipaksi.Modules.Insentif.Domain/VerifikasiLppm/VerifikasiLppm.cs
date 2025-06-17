using System.ComponentModel.DataAnnotations.Schema;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Insentif.Domain.CheckInsentif;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas;

namespace UnpakSipaksi.Modules.Insentif.Domain.VerifikasiLppm
{
    public sealed partial class VerifikasiLppm : Entity
    {
        private VerifikasiLppm()
        {
        }

        public int? Id { get; private set; } = null;

        [Column(TypeName = "VARCHAR(36)")]
        public Guid Uuid { get; private set; }
        [Column("bukti_publikasi_v")]
        public int BuktiPublikasi { get; private set; }
        [Column("status_jurnal_v", TypeName = "VARCHAR(36)")]
        public int StatusJurnal { get; private set; }
        [Column("peran_penulis_v")]
        public string PeranPenulis { get; private set; }
        [Column("cek_jumlah_penulis_v")]
        public int JumlahPenulis { get; private set; }
        [Column("jurnal_internal_v")]
        public int JenisJurnal { get; private set; }
        [Column("mahasiswa_v")]
        public int LibatkanMahasiswa { get; private set; }
        [Column("sbu_v")]
        public int SBU { get; private set; }
        [Column("porsi_sbu_v")]
        public int Porsi { get; private set; }
        [Column("cek_insentif_v")]
        public decimal Insentif { get; private set; }
        [Column("status_pengajuan_v")]
        public int StatusPengajuan { get; private set; }
        [Column("keterangan_v")]
        public string? Catatan { get; private set; }

        public static Result<VerifikasiLppm> Review(
            Domain.Insentif.Insentif prev,
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
            if (
                !EnumExtensions.GetAllEnumValues<BuktiPublikasi>().Contains(Convert.ToInt32(BuktiPublikasi))
            )
            {
                return Result.Failure<VerifikasiLppm>(VerifikasiLppmErrors.InvalidBuktiPublikasi());
            }
            if (StatusJurnal < 1)
            {
                return Result.Failure<VerifikasiLppm>(VerifikasiLppmErrors.InvalidStatusJurnal());
            }
            if (
                !EnumExtensions.GetAllEnumValues<Peran>().Contains(Convert.ToInt32(PeranPenulis))
            )
            {
                return Result.Failure<VerifikasiLppm>(VerifikasiLppmErrors.InvalidPeranPenulis());
            }
            if (JumlahPenulis < 1)
            {
                return Result.Failure<VerifikasiLppm>(VerifikasiLppmErrors.InvalidJumlahPenulis());
            }
            if (
                !EnumExtensions.GetAllEnumValues<JenisJurnal>().Contains(Convert.ToInt32(JenisJurnal))
            )
            {
                return Result.Failure<VerifikasiLppm>(VerifikasiLppmErrors.InvalidJenisJurnal());
            }
            if (
                !EnumExtensions.GetAllEnumValues<LibatkanMahasiswa>().Contains(Convert.ToInt32(LibatkanMahasiswa))
            )
            {
                return Result.Failure<VerifikasiLppm>(VerifikasiLppmErrors.InvalidLibatkanMahasiswa());
            }
            if (SBU < 1)
            {
                return Result.Failure<VerifikasiLppm>(VerifikasiLppmErrors.InvalidSBU());
            }

            if (
                !EnumExtensions.GetAllEnumValues<StatusPengajuan>().Contains(Convert.ToInt32(StatusPengajuan))
            )
            {
                return Result.Failure<VerifikasiLppm>(VerifikasiLppmErrors.InvalidStatusPengajuan());
            }
            if (
                EnumExtensions.GetAllEnumValues<StatusPengajuan>().Contains(Convert.ToInt32(StatusPengajuan)) &&
                StatusPengajuan == StatusPengajuan.Tidak &&
                string.IsNullOrEmpty(Catatan)
            )
            {
                return Result.Failure<VerifikasiLppm>(VerifikasiLppmErrors.EmptyCatatan());
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
                return Result.Failure<VerifikasiLppm>(calculator.Error);

            if (calculator.Value.PorsiSBU < 1)
                return Result.Failure<VerifikasiLppm>(VerifikasiFakultasErrors.InvalidPorsi());

            if (calculator.Value.TotalInsentif <= 0)
                return Result.Failure<VerifikasiLppm>(VerifikasiFakultasErrors.InvalidCalculateMoney());

            var asset = new VerifikasiLppm
            {
                Id = prev.Id,
                Uuid = prev.Uuid,
                BuktiPublikasi = (int)BuktiPublikasi,
                StatusJurnal = StatusJurnal,
                PeranPenulis = PeranPenulis.ToString(),
                JumlahPenulis = JumlahPenulis,
                JenisJurnal = (int)JenisJurnal,
                LibatkanMahasiswa = (int)LibatkanMahasiswa,
                SBU = SBU,
                Porsi = calculator.Value.PorsiSBU,
                Insentif = calculator.Value.TotalInsentif,
                StatusPengajuan = (int)StatusPengajuan,
                Catatan = Catatan
            };

            asset.Raise(new VerifikasiLppmCreatedDomainEvent(asset.Uuid));

            return asset;
        }
    }
}
