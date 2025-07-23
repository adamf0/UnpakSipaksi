using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan
{
    public static class MetodeRencanaKegiatanErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("MetodeRencanaKegiatan.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("MetodeRencanaKegiatan.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("MetodeRencanaKegiatan.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidValueNilai() =>
            Error.NotFound("MetodeRencanaKegiatan.InvalidValueNilai", "Invalid value 'nilai'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("MetodeRencanaKegiatan.NotFound", $"Metode rencana kegiatan with the identifier {Id} was not found");

    }
}
