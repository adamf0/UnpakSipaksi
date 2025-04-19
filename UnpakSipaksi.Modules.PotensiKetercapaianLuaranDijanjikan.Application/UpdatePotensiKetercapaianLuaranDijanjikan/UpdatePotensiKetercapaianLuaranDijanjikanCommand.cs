using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.UpdatePotensiKetercapaianLuaranDijanjikan
{
    public sealed record UpdatePotensiKetercapaianLuaranDijanjikanCommand(
        Guid Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
