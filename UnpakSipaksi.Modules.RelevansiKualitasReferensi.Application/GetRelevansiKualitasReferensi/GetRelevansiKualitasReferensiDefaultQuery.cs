using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetRelevansiKualitasReferensi
{
    public sealed record GetRelevansiKualitasReferensiDefaultQuery(Guid RelevansiKualitasReferensiUuid) : IQuery<RelevansiKualitasReferensiDefaultResponse>;
}
