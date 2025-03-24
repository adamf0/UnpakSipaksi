using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetMetodeRencanaKegiatan
{
    public sealed record GetMetodeRencanaKegiatanQuery(Guid MetodeRencanaKegiatanUuid) : IQuery<MetodeRencanaKegiatanResponse>;
}
