using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KualitasIpteks.Application.GetKualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.GetAllKualitasIpteks
{
    public sealed record GetAllKualitasIpteksQuery() : IQuery<List<KualitasIpteksResponse>>;
}
