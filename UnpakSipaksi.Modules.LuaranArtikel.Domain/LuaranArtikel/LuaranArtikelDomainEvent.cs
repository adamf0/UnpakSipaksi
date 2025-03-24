using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel
{
    public sealed class LuaranArtikelCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
