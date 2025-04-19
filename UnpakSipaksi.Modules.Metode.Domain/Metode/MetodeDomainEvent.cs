using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Metode.Domain.Metode
{
    public sealed class MetodeCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
