using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Pengumuman.Application.DeletePengumuman
{
    public sealed record DeletePengumumanCommand(
        Guid uuid
    ) : ICommand;
}
