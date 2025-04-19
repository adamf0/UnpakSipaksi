using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis
{
    public static class KetajamanAnalisisErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KetajamanAnalisis.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("KetajamanAnalisis.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KetajamanAnalisis.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KetajamanAnalisis.NotFound", $"Ketajaman analisis with the identifier {Id} was not found");

    }
}
