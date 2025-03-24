using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.LuaranArtikel.Application.UpdateLuaranArtikel
{
    public sealed record UpdateLuaranArtikelCommand(
        Guid Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
