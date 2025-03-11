using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks
{
    public sealed class KualitasIpteksCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
