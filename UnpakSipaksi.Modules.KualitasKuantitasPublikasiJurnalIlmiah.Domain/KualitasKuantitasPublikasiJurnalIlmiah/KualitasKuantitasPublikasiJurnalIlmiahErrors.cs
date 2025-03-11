using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah
{
    public static class KualitasKuantitasPublikasiJurnalIlmiahErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KualitasKuantitasPublikasiJurnalIlmiah.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KualitasKuantitasPublikasiJurnalIlmiah.NotFound", $"The event with the identifier {Id} was not found");

    }
}
