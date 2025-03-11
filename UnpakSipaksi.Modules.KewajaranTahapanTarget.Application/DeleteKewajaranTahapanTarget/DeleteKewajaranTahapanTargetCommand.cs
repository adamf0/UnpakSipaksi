using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.DeleteKetajamanAnalisis
{
    public sealed record DeleteKewajaranTahapanTargetCommand(
        Guid uuid
    ) : ICommand;
}
