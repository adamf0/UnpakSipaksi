using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Substansi.Application.GetSubstansiInternal;

namespace UnpakSipaksi.Modules.Substansi.Application.GetAllSubstansiInternal
{
    public sealed record GetAllSubstansiInternalQuery(Guid UuidPenelitianHibah) : IQuery<List<SubstansiInfoInternalResponse>>;
}
