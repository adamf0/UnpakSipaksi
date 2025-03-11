using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis
{
    public sealed class KetajamanAnalisisCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
