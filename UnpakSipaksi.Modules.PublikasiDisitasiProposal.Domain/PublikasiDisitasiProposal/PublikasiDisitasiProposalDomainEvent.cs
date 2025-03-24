using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal
{
    public sealed class PublikasiDisitasiProposalCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
