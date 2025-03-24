using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan
{
    public sealed class MetodeRencanaKegiatanCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
