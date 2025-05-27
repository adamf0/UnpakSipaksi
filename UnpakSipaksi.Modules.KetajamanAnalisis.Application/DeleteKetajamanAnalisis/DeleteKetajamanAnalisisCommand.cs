using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.DeleteKetajamanAnalisis
{
    public sealed record DeleteKetajamanAnalisisCommand(
        string uuid
    ) : ICommand;
}
