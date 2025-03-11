using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Komponen.Application.GetKomponen;

namespace UnpakSipaksi.Modules.Komponen.Application.GetAllKomponen
{
    public sealed record GetAllKomponenQuery() : IQuery<List<KomponenResponse>>;
}
