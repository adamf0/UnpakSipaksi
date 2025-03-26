using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.GetVideoKegiatan
{
    public sealed record GetVideoKegiatanQuery(Guid VideoKegiatanUuid) : IQuery<VideoKegiatanResponse>;
}
