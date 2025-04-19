using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah
{
    public static class KetajamanPerumusanMasalahErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KetajamanPerumusanMasalah.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("KetajamanPerumusanMasalah.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KetajamanPerumusanMasalah.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KetajamanPerumusanMasalah.NotFound", $"Ketajaman perumusan masalah with the identifier {Id} was not found");

    }
}
