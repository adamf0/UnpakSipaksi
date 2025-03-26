using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan
{
    public sealed class VideoKegiatanCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
