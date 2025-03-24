using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.LuaranArtikel.Application.GetLuaranArtikel;

namespace UnpakSipaksi.Modules.LuaranArtikel.Application.GetAllLuaranArtikel
{
    public sealed record GetAllLuaranArtikelQuery() : IQuery<List<LuaranArtikelResponse>>;
}
