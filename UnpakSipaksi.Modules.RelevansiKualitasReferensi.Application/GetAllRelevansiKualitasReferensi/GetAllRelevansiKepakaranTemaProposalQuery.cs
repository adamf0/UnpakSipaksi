using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetRelevansiKualitasReferensi;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetAllRelevansiKualitasReferensi
{
    public sealed record GetAllRelevansiKualitasReferensiQuery() : IQuery<List<RelevansiKualitasReferensiResponse>>;
}
