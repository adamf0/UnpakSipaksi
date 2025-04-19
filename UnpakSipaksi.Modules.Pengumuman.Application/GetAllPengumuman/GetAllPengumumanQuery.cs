using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Pengumuman.Application.GetPengumuman;

namespace UnpakSipaksi.Modules.Pengumuman.Application.GetAllPengumuman
{
    public sealed record GetAllPengumumanQuery() : IQuery<List<PengumumanResponse>>;
}
