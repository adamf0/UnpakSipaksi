namespace UnpakSipaksi.Modules.LuaranArtikel.Application.GetLuaranArtikel
{
    public sealed record LuaranArtikelResponse
    {
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public string Nilai { get; set; } = default!;
    }
}
