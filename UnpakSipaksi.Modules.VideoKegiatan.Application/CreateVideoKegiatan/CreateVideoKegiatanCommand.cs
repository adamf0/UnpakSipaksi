using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.CreateVideoKegiatan
{
    public sealed record CreateVideoKegiatanCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
