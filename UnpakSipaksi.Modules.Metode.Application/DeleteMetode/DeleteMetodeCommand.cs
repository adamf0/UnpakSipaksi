using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Metode.Application.DeleteMetode
{
    public sealed record DeleteMetodeCommand(
        Guid uuid
    ) : ICommand;
}
