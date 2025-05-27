using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.LuaranArtikel.Application.DeleteLuaranArtikel
{
    public sealed record DeleteLuaranArtikelCommand(
        string uuid
    ) : ICommand;
}
