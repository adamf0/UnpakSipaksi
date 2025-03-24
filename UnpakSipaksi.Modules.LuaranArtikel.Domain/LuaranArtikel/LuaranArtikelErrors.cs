using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel
{
    public static class LuaranArtikelErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("LuaranArtikel.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("LuaranArtikel.NotFound", $"The event with the identifier {Id} was not found");

    }
}
