using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.DeleteKewajaranTahapanTarget
{
    public sealed record DeleteKewajaranTahapanTargetCommand(
        Guid uuid
    ) : ICommand;
}
