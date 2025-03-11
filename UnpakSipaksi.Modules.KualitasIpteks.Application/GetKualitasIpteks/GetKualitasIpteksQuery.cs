using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.GetKualitasIpteks
{
    public sealed record GetKualitasIpteksQuery(Guid KualitasIpteksUuid) : IQuery<KualitasIpteksResponse>;
}
