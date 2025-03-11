using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.CreateKetajamanAnalisis
{
    public sealed record CreateKetajamanAnalisisCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
