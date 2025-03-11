using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah
{
    public sealed class KetajamanPerumusanMasalahCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
