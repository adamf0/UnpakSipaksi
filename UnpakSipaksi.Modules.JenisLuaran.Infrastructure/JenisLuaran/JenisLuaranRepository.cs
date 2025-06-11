using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.JenisLuaran.Domain;
using UnpakSipaksi.Modules.JenisLuaran.Infrastructure.Database;

namespace UnpakSipaksi.Modules.JenisLuaran.Infrastructure.JenisLuaran
{
    internal sealed class JenisLuaranRepository(JenisLuaranDbContext context) : IJenisLuaranRepository
    {
        public async Task<Domain.JenisLuaran> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.JenisLuaran akurasiPenelitian = await context.JenisLuaran.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return akurasiPenelitian;
        }

        public async Task DeleteAsync(Domain.JenisLuaran akurasiPenelitian)
        {
            context.JenisLuaran.Remove(akurasiPenelitian);
        }

        public void Insert(Domain.JenisLuaran akurasiPenelitian)
        {
            context.JenisLuaran.Add(akurasiPenelitian);
        }
    }
}
