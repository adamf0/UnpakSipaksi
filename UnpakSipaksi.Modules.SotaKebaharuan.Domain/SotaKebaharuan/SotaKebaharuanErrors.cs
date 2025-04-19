using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan
{
    public static class SotaKebaharuanErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("SotaKebaharuan.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("SotaKebaharuan.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("SotaKebaharuan.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("SotaKebaharuan.NotFound", $"Sota kebaharuan with the identifier {Id} was not found");

    }
}
