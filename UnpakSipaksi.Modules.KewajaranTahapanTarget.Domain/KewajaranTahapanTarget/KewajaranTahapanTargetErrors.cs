using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget
{
    public static class KewajaranTahapanTargetErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KewajaranTahapanTarget.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("KewajaranTahapanTarget.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KewajaranTahapanTarget.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidValueNilai() =>
            Error.NotFound("KewajaranTahapanTarget.InvalidValueNilai", "Invalid value 'nilai'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KewajaranTahapanTarget.NotFound", $"Kewajaran tahapan target with the identifier {Id} was not found");

    }
}
