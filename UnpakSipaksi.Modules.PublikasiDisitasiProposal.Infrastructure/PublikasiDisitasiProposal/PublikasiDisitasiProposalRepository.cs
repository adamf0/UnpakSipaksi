using Microsoft.EntityFrameworkCore;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Infrastructure.Database;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Infrastructure.PublikasiDisitasiProposal
{
    internal sealed class PublikasiDisitasiProposalRepository(PublikasiDisitasiProposalDbContext context) : IPublikasiDisitasiProposalRepository
    {
        public async Task<Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal PublikasiDisitasiProposal = await context.PublikasiDisitasiProposal.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return PublikasiDisitasiProposal;
        }

        public async Task DeleteAsync(Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal PublikasiDisitasiProposal)
        {
            context.PublikasiDisitasiProposal.Remove(PublikasiDisitasiProposal);
        }

        public void Insert(Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal PublikasiDisitasiProposal)
        {
            context.PublikasiDisitasiProposal.Add(PublikasiDisitasiProposal);
        }
    }
}
