using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra
{
    public sealed class RumusanPrioritasMitraCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
