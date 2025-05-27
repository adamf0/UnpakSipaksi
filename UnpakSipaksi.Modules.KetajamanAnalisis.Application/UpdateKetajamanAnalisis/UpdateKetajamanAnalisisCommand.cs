using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.UpdateKetajamanAnalisis
{
    public sealed record UpdateKetajamanAnalisisCommand(
        string Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
