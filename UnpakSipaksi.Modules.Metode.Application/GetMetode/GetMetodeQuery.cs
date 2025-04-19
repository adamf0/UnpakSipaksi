using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Metode.Application.GetMetode
{
    public sealed record GetMetodeQuery(Guid MetodeUuid) : IQuery<MetodeResponse>;
}
