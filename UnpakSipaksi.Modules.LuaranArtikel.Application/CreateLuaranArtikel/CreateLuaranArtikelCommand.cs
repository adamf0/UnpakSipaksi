using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.LuaranArtikel.Application.CreateLuaranArtikel
{
    public sealed record CreateLuaranArtikelCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
