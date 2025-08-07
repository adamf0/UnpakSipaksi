using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Domain.Dokumen
{
    public sealed class KetajamanAnalisisCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
