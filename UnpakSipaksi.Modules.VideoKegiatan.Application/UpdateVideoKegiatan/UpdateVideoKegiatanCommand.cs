using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.UpdateVideoKegiatan
{
    public sealed record UpdateVideoKegiatanCommand(
        string Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
