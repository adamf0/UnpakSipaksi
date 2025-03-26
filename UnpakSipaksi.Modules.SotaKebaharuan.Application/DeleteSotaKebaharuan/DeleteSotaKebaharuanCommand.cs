using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.DeleteSotaKebaharuan
{
    public sealed record DeleteSotaKebaharuanCommand(
        Guid uuid
    ) : ICommand;
}
