using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel
{
    public static class LuaranArtikelErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("LuaranArtikel.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("LuaranArtikel.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("LuaranArtikel.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("LuaranArtikel.NotFound", $"Luaran artikel with the identifier {Id} was not found");

    }
}
