using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal
{
    public sealed class RelevansiKepakaranTemaProposalCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
