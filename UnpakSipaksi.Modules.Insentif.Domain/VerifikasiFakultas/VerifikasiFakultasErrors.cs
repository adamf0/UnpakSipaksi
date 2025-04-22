using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.VerifikasiFakultas.Domain.VerifikasiFakultas
{
    public static class VerifikasiFakultasErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("VerifikasiFakultas.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("VerifikasiFakultas.NotFound", $"Insentif with identifier {Id} not found");
        public static Error InvalidBuktiPublikasi() =>
            Error.NotFound("VerifikasiFakultas.InvalidBuktiPublikasi", "Bukti publikasi is invalid value");
        public static Error InvalidStatusJurnal() =>
            Error.NotFound("VerifikasiFakultas.InvalidStatusJurnal", "Status jurnal is invalid value");
        public static Error InvalidPeranPenulis() =>
            Error.NotFound("VerifikasiFakultas.InvalidPeranPenulis", "Peran penulis is invalid value");
        public static Error InvalidJumlahPenulis() =>
            Error.NotFound("VerifikasiFakultas.InvalidJumlahPenulis", "Jumlah penulis is invalid value");
        public static Error InvalidJenisJurnal() =>
            Error.NotFound("VerifikasiFakultas.InvalidJenisJurnal", "Jenis jurnal is invalid value");
        public static Error InvalidLibatkanMahasiswa() =>
            Error.NotFound("VerifikasiFakultas.InvalidLibatkanMahasiswa", "Libatkan mahasiswa is invalid value");
        public static Error InvalidSBU() =>
            Error.NotFound("VerifikasiFakultas.InvalidSBU", "SBU is invalid value");
        public static Error InvalidPorsi() =>
            Error.NotFound("VerifikasiFakultas.InvalidPorsi", "Porsi is invalid value");
        public static Error InvalidCalculateMoney() =>
            Error.NotFound("VerifikasiFakultas.InvalidCaclculateMoney", "There was a miscalculation of the first author / co-author money ");
        public static Error InvalidStatusPengajuan() =>
            Error.NotFound("VerifikasiFakultas.InvalidStatusPengajuan", "Status pengajuan is invalid value");
        public static Error EmptyCatatan() =>
            Error.NotFound("VerifikasiFakultas.EmptyCatatan", "Catatan is empty");
    }
}
