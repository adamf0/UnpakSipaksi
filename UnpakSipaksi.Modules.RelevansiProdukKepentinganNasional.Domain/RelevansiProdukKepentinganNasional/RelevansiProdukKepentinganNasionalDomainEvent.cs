using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional
{
    public sealed class RelevansiProdukKepentinganNasionalCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
