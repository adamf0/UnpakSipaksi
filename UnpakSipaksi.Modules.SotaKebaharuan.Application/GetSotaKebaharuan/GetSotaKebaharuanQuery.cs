using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.GetSotaKebaharuan
{
    public sealed record GetSotaKebaharuanQuery(Guid SotaKebaharuanUuid) : IQuery<SotaKebaharuanResponse>;
}
