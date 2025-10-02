using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Domain.Luaran
{
    public static class LuaranErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("LaporanKemajuanLuaran.EmptyData", "data is not found");
        public static Error InvalidJenis() =>
            Error.NotFound("LaporanKemajuanLuaran.InvalidJenis", "Invalid value 'jenis'");
        public static Error InvalidType() =>
            Error.NotFound("LaporanKemajuanLuaran.InvalidType", "Invalid value 'type'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("LaporanKemajuanLuaran.NotFound", $"Luaran laporan akhir with the identifier {Id} was not found");

    }
}
