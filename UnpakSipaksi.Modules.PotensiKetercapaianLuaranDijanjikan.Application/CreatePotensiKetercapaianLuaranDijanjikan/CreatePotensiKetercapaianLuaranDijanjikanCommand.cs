using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.CreatePotensiKetercapaianLuaranDijanjikan
{
    public sealed record CreatePotensiKetercapaianLuaranDijanjikanCommand(
        string Nama,
        int Skor
    ) : ICommand<Guid>;
}
