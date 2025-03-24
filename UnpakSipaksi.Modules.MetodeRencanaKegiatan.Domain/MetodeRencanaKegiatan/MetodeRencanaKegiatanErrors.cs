using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan
{
    public static class MetodeRencanaKegiatanErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("MetodeRencanaKegiatan.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("MetodeRencanaKegiatan.NotFound", $"The event with the identifier {Id} was not found");

    }
}
