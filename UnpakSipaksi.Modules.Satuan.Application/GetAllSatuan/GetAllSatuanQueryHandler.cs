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
using UnpakSipaksi.Modules.Satuan.Application.GetSatuan;
using UnpakSipaksi.Modules.Satuan.Domain.Satuan;

namespace UnpakSipaksi.Modules.Satuan.Application.GetAllSatuan
{
    internal sealed class GetAllSatuanQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllSatuanQuery, List<SatuanResponse>>
    {
        public async Task<Result<List<SatuanResponse>>> Handle(GetAllSatuanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama AS Nama
            FROM satuan
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<SatuanResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<SatuanResponse>>(SatuanErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
