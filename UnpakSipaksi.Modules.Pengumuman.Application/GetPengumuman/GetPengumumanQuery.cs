using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Pengumuman.Application.GetPengumuman
{
    public sealed record GetPengumumanQuery(Guid PengumumanUuid) : IQuery<PengumumanResponse>;
}
