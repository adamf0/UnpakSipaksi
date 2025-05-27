using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.UpdateKualitasIpteks
{
    public sealed record UpdateKualitasIpteksCommand(
        string Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
