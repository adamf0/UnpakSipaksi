using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi
{
    public sealed class RelevansiKualitasReferensiCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
