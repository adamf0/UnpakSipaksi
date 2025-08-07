using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Domain.Dokumen
{
    public static class DokumenErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("LaporanKemajuanDokumen.EmptyData", "data is not found");
        public static Error InvalidType() =>
            Error.NotFound("LaporanKemajuanDokumen.InvalidType", "Invalid value 'type'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("LaporanKemajuanDokumen.NotFound", $"Dokumen laporan akhir with the identifier {Id} was not found");

    }
}
