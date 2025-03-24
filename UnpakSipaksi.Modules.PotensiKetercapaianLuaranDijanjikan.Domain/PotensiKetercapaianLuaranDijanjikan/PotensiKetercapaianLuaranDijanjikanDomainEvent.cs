using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan
{
    public sealed class PotensiKetercapaianLuaranDijanjikanCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
