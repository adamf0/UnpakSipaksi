using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Metode.Application.GetMetode;

namespace UnpakSipaksi.Modules.Metode.Application.GetAllMetode
{
    public sealed record GetAllMetodeQuery() : IQuery<List<MetodeResponse>>;
}
