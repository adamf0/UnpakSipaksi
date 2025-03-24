using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetRelevansiKualitasReferensi
{
    public sealed record GetRelevansiKualitasReferensiQuery(Guid RelevansiKualitasReferensiUuid) : IQuery<RelevansiKualitasReferensiResponse>;
}
