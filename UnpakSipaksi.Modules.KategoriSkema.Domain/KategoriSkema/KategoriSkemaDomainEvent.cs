using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema
{
    public sealed class KategoriSkemaCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
