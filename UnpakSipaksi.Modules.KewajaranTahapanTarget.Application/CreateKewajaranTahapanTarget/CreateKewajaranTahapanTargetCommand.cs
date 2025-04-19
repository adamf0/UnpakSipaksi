using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.CreateKewajaranTahapanTarget
{
    public sealed record CreateKewajaranTahapanTargetCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
