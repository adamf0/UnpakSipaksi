using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.DeleteKualitasKuantitasPublikasiProsiding
{
    public sealed record DeleteKualitasKuantitasPublikasiProsidingCommand(
        string uuid
    ) : ICommand;
}
