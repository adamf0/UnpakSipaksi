using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding
{
    public static class KualitasKuantitasPublikasiProsidingErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KualitasKuantitasPublikasiProsiding.EmptyData", "data is not found");

        public static Error NotSameValue() =>
            Error.NotFound("KualitasKuantitasPublikasiProsiding.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KualitasKuantitasPublikasiProsiding.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KualitasKuantitasPublikasiProsiding.NotFound", $"Kualitas kuantitas publikasi prosiding with the identifier {Id} was not found");

    }
}
