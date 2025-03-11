using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding
{
    public static class KualitasKuantitasPublikasiProsidingErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KualitasKuantitasPublikasiProsiding.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KualitasKuantitasPublikasiProsiding.NotFound", $"The event with the identifier {Id} was not found");

    }
}
