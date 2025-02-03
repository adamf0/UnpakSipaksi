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
using UnpakSipaksi.Modules.KategoriSumberDana.Domain.KategoriSumberDana;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.GetKategoriSumberDana;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Application.GetAllKategoriSumberDana
{
    internal sealed class GetAllKategoriSumberDanaQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKategoriSumberDanaQuery, List<KategoriSumberDanaResponse>>
    {
        public async Task<Result<List<KategoriSumberDanaResponse>>> Handle(GetAllKategoriSumberDanaQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama as Nama
            FROM kategori_sumber_dana
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KategoriSumberDanaResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KategoriSumberDanaResponse>>(KategoriSumberDanaErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
