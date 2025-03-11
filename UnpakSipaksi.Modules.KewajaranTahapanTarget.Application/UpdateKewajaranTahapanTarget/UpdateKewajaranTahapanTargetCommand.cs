using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.UpdateKetajamanAnalisis
{
    public sealed record UpdateKewajaranTahapanTargetCommand(
        Guid Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
