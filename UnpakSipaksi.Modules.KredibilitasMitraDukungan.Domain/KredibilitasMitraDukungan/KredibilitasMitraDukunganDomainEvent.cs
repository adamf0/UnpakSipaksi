using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan
{
    public sealed class KredibilitasMitraDukunganCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
