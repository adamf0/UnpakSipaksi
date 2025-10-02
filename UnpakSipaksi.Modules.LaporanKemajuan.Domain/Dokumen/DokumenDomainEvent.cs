using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Domain.Dokumen
{
    public sealed class DokumenCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
