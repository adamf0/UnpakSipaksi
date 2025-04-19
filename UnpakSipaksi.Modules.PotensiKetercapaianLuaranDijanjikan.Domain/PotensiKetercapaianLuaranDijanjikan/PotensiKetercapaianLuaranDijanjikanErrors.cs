using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan
{
    public static class PotensiKetercapaianLuaranDijanjikanErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("PotensiKetercapaianLuaranDijanjikan.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("PotensiKetercapaianLuaranDijanjikan.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("PotensiKetercapaianLuaranDijanjikan.UnknownKategoriSkema", "Unknown schema category");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("PotensiKetercapaianLuaranDijanjikan.NotFound", $"Potensi ketercapaian luaran dijanjikan with the identifier {Id} was not found");

    }
}
