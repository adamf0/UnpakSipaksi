using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Domain.KategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.GetKategoriMitraPenelitian;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.GetAllKategoriMitraPenelitian
{
    internal sealed class GetAllKategoriMitraPenelitianQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKategoriMitraPenelitianQuery, List<KategoriMitraPenelitianResponse>>
    {
        public async Task<Result<List<KategoriMitraPenelitianResponse>>> Handle(GetAllKategoriMitraPenelitianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama as Nama
            FROM kategori_mitra_penelitianpkmmandiri
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KategoriMitraPenelitianResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KategoriMitraPenelitianResponse>>(KategoriMitraPenelitianErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
