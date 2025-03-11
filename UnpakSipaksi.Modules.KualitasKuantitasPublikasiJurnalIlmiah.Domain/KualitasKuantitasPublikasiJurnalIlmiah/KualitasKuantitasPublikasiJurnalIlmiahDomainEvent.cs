using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah
{
    public sealed class KualitasKuantitasPublikasiJurnalIlmiahDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
