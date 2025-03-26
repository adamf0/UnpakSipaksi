using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan
{
    public static class SotaKebaharuanErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("SotaKebaharuan.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("SotaKebaharuan.NotFound", $"The event with the identifier {Id} was not found");

    }
}
