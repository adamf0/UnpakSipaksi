using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Application.DeleteUrgensiPenelitian
{
    public sealed record DeleteUrgensiPenelitianCommand(
        Guid uuid
    ) : ICommand;
}
