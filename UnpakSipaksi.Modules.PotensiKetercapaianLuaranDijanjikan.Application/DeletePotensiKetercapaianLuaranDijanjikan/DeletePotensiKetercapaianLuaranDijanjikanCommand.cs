using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.DeletePotensiKetercapaianLuaranDijanjikan
{
    public sealed record DeletePotensiKetercapaianLuaranDijanjikanCommand(
        string uuid
    ) : ICommand;
}
