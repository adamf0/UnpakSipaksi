using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah
{
    public static class KetajamanPerumusanMasalahErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KetajamanPerumusanMasalah.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KetajamanPerumusanMasalah.NotFound", $"The event with the identifier {Id} was not found");

    }
}
