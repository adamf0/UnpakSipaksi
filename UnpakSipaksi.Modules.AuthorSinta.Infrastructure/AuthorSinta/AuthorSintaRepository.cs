using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.Infrastructure.Database;

namespace UnpakSipaksi.Modules.AuthorSinta.Infrastructure.AuthorSinta
{
    internal sealed class AuthorSintaRepository(AuthorSintaDbContext context) : IAuthorSintaRepository
    {
        public async Task<Domain.AuthorSinta.AuthorSinta> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.AuthorSinta.AuthorSinta akurasiPenelitian = await context.AuthorSinta.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task DeleteAsync(Domain.AuthorSinta.AuthorSinta akurasiPenelitian)
        {
            context.AuthorSinta.Remove(akurasiPenelitian);
        }

        public void Insert(Domain.AuthorSinta.AuthorSinta akurasiPenelitian)
        {
            context.AuthorSinta.Add(akurasiPenelitian);
        }
    }
}
