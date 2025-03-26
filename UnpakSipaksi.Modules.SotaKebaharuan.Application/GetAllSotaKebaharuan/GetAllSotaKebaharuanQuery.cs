using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.GetSotaKebaharuan;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.GetAllSotaKebaharuan
{
    public sealed record GetAllSotaKebaharuanQuery() : IQuery<List<SotaKebaharuanResponse>>;
}
