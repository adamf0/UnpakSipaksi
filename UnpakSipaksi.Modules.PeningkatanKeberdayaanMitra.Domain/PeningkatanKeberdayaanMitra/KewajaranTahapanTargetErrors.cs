using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra
{
    public static class PeningkatanKeberdayaanMitraErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("PeningkatanKeberdayaanMitra.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("PeningkatanKeberdayaanMitra.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("PeningkatanKeberdayaanMitra.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidValueNilai() =>
            Error.NotFound("PeningkatanKeberdayaanMitra.InvalidValueNilai", "Invalid value 'nilai'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("PeningkatanKeberdayaanMitra.NotFound", $"Peningkatan keberdayaan mitra with the identifier {Id} was not found");

    }
}
