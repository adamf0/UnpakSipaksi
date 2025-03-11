using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.UpdateKualitasIpteks
{
    public sealed record UpdateKualitasIpteksCommand(
        Guid Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
