using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Domain.UrgensiPenelitian
{
    public sealed class UrgensiPenelitianCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
