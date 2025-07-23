using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi
{
    public static class KuantitasStatusKiErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KuantitasStatusKi.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("KuantitasStatusKi.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KuantitasStatusKi.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidValueNilai() =>
            Error.NotFound("KuantitasStatusKi.InvalidValueNilai", "Invalid value 'nilai'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KuantitasStatusKi.NotFound", $"Kuantitas status KI with the identifier {Id} was not found");

    }
}
