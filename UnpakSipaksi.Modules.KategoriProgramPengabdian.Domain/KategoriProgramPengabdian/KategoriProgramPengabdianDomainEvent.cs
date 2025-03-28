using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian
{
    public sealed class KategoriProgramPengabdianCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
