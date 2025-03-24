using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan
{
    public static class PotensiKetercapaianLuaranDijanjikanErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("PotensiKetercapaianLuaranDijanjikan.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("PotensiKetercapaianLuaranDijanjikan.NotFound", $"The event with the identifier {Id} was not found");

    }
}
