using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan
{
    public sealed class SotaKebaharuanCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
