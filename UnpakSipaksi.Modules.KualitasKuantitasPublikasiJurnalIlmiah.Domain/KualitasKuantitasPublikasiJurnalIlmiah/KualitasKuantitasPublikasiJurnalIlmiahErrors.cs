using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah
{
    public static class KualitasKuantitasPublikasiJurnalIlmiahErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KualitasKuantitasPublikasiJurnalIlmiah.EmptyData", "data is not found");

        public static Error NotSameValue() =>
            Error.NotFound("KualitasKuantitasPublikasiJurnalIlmiah.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KualitasKuantitasPublikasiJurnalIlmiah.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidValueNilai() =>
            Error.NotFound("KualitasKuantitasPublikasiJurnalIlmiah.InvalidValueNilai", "Invalid value 'nilai'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KualitasKuantitasPublikasiJurnalIlmiah.NotFound", $"Kualitas kuantitas publikasi jurnal ilmiah with the identifier {Id} was not found");

    }
}
