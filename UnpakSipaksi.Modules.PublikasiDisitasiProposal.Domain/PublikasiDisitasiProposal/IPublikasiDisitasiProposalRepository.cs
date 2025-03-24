namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal
{
    public interface IPublikasiDisitasiProposalRepository
    {
        void Insert(PublikasiDisitasiProposal PublikasiDisitasiProposal);
        Task<PublikasiDisitasiProposal> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(PublikasiDisitasiProposal PublikasiDisitasiProposal);
    }
}
