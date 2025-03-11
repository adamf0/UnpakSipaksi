using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Komponen.Application.GetKomponen
{
    public sealed record GetKomponenQuery(Guid KomponenUuid) : IQuery<KomponenResponse>;
}
