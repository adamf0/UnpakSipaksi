using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Substansi.Application.GetSubstansiInternal
{
    public sealed record GetSubstansiInternalQuery(Guid UuidSubstansiInternal, Guid UuidPenelitianHibah) : IQuery<SubstansiInternalResponse>;
}
