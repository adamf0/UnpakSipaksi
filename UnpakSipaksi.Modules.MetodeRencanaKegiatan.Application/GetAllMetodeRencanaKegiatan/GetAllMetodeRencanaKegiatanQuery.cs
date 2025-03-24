using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetMetodeRencanaKegiatan;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetAllMetodeRencanaKegiatan
{
    public sealed record GetAllMetodeRencanaKegiatanQuery() : IQuery<List<MetodeRencanaKegiatanResponse>>;
}
