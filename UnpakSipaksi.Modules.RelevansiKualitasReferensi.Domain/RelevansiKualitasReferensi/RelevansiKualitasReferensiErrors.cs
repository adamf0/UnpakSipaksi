using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi
{
    public static class RelevansiKualitasReferensiErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("RelevansiKualitasReferensi.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("RelevansiKualitasReferensi.NotSameValue", "not the same value in data 'skor'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("RelevansiKualitasReferensi.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidValueSkor() =>
            Error.NotFound("RelevansiKualitasReferensi.InvalidValueSkor", "Invalid value 'skor'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("RelevansiKualitasReferensi.NotFound", $"Relevansi kualitas referensi with the identifier {Id} was not found");
    }
}
