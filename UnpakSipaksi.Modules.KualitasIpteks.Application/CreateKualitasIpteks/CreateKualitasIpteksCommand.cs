using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.CreateKualitasIpteks
{
    public sealed record CreateKualitasIpteksCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
