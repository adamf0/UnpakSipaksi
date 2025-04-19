using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman
{
    public sealed class PengumumanCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
