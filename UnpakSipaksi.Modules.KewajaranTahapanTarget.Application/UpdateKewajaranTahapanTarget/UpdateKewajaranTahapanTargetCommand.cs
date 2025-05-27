using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.UpdateKewajaranTahapanTarget
{
    public sealed record UpdateKewajaranTahapanTargetCommand(
        string Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
