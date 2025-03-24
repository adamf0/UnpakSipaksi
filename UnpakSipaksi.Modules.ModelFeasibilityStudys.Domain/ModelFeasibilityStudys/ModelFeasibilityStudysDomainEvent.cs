using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys
{
    public sealed class ModelFeasibilityStudysCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
