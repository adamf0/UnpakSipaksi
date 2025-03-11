using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.UpdateKetajamanAnalisis
{
    public sealed record UpdateKetajamanAnalisisCommand(
        Guid Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
