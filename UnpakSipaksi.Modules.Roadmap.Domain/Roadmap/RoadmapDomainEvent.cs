using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Roadmap.Domain.Roadmap
{
    public sealed class RoadmapCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
