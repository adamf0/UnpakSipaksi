using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks
{
    public static class KualitasIpteksErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KualitasIpteks.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KualitasIpteks.NotFound", $"The event with the identifier {Id} was not found");

    }
}
