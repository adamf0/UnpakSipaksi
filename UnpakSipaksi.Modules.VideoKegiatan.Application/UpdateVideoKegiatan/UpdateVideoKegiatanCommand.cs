using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.UpdateVideoKegiatan
{
    public sealed record UpdateVideoKegiatanCommand(
        Guid Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
