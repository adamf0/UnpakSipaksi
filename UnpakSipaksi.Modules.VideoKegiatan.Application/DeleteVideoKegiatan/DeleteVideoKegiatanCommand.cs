using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.DeleteVideoKegiatan
{
    public sealed record DeleteVideoKegiatanCommand(
        Guid uuid
    ) : ICommand;
}
