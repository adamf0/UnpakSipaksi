using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget
{
    public sealed class KewajaranTahapanTargetCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
