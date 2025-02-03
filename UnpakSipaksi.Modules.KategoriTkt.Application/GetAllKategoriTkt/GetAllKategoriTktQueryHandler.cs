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
using UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Application.GetKategoriTkt;

namespace UnpakSipaksi.Modules.KategoriTkt.Application.GetAllKategoriTkt
{
    internal sealed class GetAllKategoriTktQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKategoriTktQuery, List<KategoriTktResponse>>
    {
        public async Task<Result<List<KategoriTktResponse>>> Handle(GetAllKategoriTktQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama as Nama
            FROM kategori_tkt
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KategoriTktResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KategoriTktResponse>>(KategoriTktErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
