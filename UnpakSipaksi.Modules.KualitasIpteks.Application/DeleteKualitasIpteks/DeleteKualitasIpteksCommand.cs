using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.DeleteKualitasIpteks
{
    public sealed record DeleteKualitasIpteksCommand(
        Guid uuid
    ) : ICommand;
}
