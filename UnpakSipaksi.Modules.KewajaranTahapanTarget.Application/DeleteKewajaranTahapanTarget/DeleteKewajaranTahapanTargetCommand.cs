using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.DeleteKewajaranTahapanTarget
{
    public sealed record DeleteKewajaranTahapanTargetCommand(
        string uuid
    ) : ICommand;
}
