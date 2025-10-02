using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Pengumuman.Application.DeletePengumuman
{
    public sealed record DeletePengumumanCommand(
        string uuid
    ) : ICommand;
}
