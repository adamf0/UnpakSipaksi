using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian
{
    public sealed class RoadmapPenelitianCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
