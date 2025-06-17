using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Substansi.Domain.SubstansiInternal
{
    public sealed class SubstansiCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
