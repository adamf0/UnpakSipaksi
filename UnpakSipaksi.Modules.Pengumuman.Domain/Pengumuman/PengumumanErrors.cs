using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman
{
    public static class PengumumanErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("Pengumuman.EmptyData", "data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("Pengumuman.NotFound", $"Pengumuman with the identifier {Id} was not found");

        public static Error RequireDateRangeExpired() =>
            Error.NotFound("Pengumuman.RequireRangeDateexpired", "Nilai TanggalAwal dan TanggalAkhir tidak boleh kosong");

        public static Error InvalidDateRangeExpired() =>
            Error.NotFound("Pengumuman.InvalidDateRangeExpired", "Nilai TanggalAkhir tidak boleh dibawah TanggalAwal");

        public static Error InvalidNidn() =>
            Error.NotFound("Pengumuman.InvalidNidn", "Nidn is invalid format");

    }
}
