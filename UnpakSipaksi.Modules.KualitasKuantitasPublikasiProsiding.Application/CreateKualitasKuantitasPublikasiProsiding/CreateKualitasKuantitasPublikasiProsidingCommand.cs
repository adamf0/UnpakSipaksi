using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.CreateKualitasKuantitasPublikasiProsiding
{
    public sealed record CreateKualitasKuantitasPublikasiProsidingCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
