using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra
{
    public sealed class PeningkatanKeberdayaanMitraCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
