using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Komponen.Domain.Komponen
{
    public static class KomponenErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("Komponen.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("Komponen.NotFound", $"The event with the identifier {Id} was not found");

    }
}
