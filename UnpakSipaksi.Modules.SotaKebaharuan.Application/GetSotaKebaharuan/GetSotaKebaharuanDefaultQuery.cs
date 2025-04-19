using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.GetSotaKebaharuan
{
    public sealed record GetSotaKebaharuanDefaultQuery(Guid SotaKebaharuanUuid) : IQuery<SotaKebaharuanDefaultResponse>;
}
