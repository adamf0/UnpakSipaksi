using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.UpdatePotensiKetercapaianLuaranDijanjikan
{
    public sealed record UpdatePotensiKetercapaianLuaranDijanjikanCommand(
        string Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
