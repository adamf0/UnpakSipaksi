using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Substansi.Application.DeleteSubstansiInternal
{
    public sealed record DeleteSubstansiInternalCommand(
        string Uuid
    ) : ICommand;
}
