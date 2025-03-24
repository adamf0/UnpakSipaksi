using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi
{
    public static class RelevansiKualitasReferensiErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("RelevansiKualitasReferensi.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("RelevansiKualitasReferensi.NotFound", $"The event with the identifier {Id} was not found");

    }
}
