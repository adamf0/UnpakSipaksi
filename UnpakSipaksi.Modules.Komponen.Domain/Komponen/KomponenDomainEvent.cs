using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Komponen.Domain.Komponen
{
    public sealed class KomponenCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
