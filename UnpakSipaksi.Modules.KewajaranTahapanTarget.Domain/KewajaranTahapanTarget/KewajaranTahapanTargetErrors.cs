using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget
{
    public static class KewajaranTahapanTargetErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KewajaranTahapanTarget.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KewajaranTahapanTarget.NotFound", $"The event with the identifier {Id} was not found");

    }
}
