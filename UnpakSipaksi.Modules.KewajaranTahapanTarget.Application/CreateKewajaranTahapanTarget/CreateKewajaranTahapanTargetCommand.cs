using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.CreateKetajamanAnalisis
{
    public sealed record CreateKewajaranTahapanTargetCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
