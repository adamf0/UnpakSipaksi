using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.VideoKegiatan.Application.GetVideoKegiatan;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.GetAllVideoKegiatan
{
    public sealed record GetAllVideoKegiatanQuery() : IQuery<List<VideoKegiatanResponse>>;
}
