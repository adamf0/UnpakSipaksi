using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks
{
    public static class KualitasIpteksErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KualitasIpteks.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("KualitasIpteks.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KualitasIpteks.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KualitasIpteks.NotFound", $"Kualitas ipteks with the identifier {Id} was not found");

    }
}
