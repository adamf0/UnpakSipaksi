namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal
{
    public interface IRelevansiKepakaranTemaProposalRepository
    {
        void Insert(RelevansiKepakaranTemaProposal RelevansiKepakaranTemaProposal);
        Task<RelevansiKepakaranTemaProposal> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(RelevansiKepakaranTemaProposal RelevansiKepakaranTemaProposal);
    }
}
