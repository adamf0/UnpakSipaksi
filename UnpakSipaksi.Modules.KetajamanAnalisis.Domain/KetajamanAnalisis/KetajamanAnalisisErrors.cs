using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis
{
    public static class KetajamanAnalisisErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KetajamanAnalisis.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KetajamanAnalisis.NotFound", $"The event with the identifier {Id} was not found");

    }
}
