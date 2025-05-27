using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.DeleteSotaKebaharuan
{
    public sealed record DeleteSotaKebaharuanCommand(
        string uuid
    ) : ICommand;
}
