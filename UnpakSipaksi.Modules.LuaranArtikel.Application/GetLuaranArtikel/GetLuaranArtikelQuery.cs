using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.LuaranArtikel.Application.GetLuaranArtikel
{
    public sealed record GetLuaranArtikelQuery(Guid LuaranArtikelUuid) : IQuery<LuaranArtikelResponse>;
}
