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
using UnpakSipaksi.Modules.Kategori.Application.GetKategori;
using UnpakSipaksi.Modules.Kategori.Domain.Kategori;

namespace UnpakSipaksi.Modules.Kategori.Application.GetAllKategori
{
    internal sealed class GetAllKategoriQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKategoriQuery, List<KategoriResponse>>
    {
        public async Task<Result<List<KategoriResponse>>> Handle(GetAllKategoriQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama as Nama 
            FROM penelitian_internal_kategori
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KategoriResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KategoriResponse>>(KategoriErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
