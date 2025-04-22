using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.VerifikasiLppm.Domain.VerifikasiLppm
{
    public static class VerifikasiLppmErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("VerifikasiLppm.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("VerifikasiLppm.NotFound", $"Insentif with identifier {Id} not found");
        public static Error InvalidBuktiPublikasi() =>
            Error.NotFound("VerifikasiLppm.InvalidBuktiPublikasi", "Bukti publikasi is invalid value");
        public static Error InvalidStatusJurnal() =>
            Error.NotFound("VerifikasiLppm.InvalidStatusJurnal", "Status jurnal is invalid value");
        public static Error InvalidPeranPenulis() =>
            Error.NotFound("VerifikasiLppm.InvalidPeranPenulis", "Peran penulis is invalid value");
        public static Error InvalidJumlahPenulis() =>
            Error.NotFound("VerifikasiLppm.InvalidJumlahPenulis", "Jumlah penulis is invalid value");
        public static Error InvalidJenisJurnal() =>
            Error.NotFound("VerifikasiLppm.InvalidJenisJurnal", "Jenis jurnal is invalid value");
        public static Error InvalidLibatkanMahasiswa() =>
            Error.NotFound("VerifikasiLppm.InvalidLibatkanMahasiswa", "Libatkan mahasiswa is invalid value");
        public static Error InvalidSBU() =>
            Error.NotFound("VerifikasiLppm.InvalidSBU", "SBU is invalid value");
        public static Error InvalidPorsi() =>
            Error.NotFound("VerifikasiLppm.InvalidPorsi", "Porsi is invalid value");
        public static Error InvalidCalculateMoney() =>
            Error.NotFound("VerifikasiLppm.InvalidCaclculateMoney", "There was a miscalculation of the first author / co-author money ");
        public static Error InvalidStatusPengajuan() =>
            Error.NotFound("VerifikasiLppm.InvalidStatusPengajuan", "Status pengajuan is invalid value");
        public static Error EmptyCatatan() =>
            Error.NotFound("VerifikasiLppm.EmptyCatatan", "Catatan is empty");

    }
}
