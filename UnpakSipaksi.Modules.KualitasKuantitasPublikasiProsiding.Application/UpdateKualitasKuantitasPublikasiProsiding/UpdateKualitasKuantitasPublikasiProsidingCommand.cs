using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.UpdateKualitasKuantitasPublikasiProsiding
{
    public sealed record UpdateKualitasKuantitasPublikasiProsidingCommand(
        Guid Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
