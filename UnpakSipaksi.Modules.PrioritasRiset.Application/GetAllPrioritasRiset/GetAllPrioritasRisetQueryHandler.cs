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
using UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset;
using UnpakSipaksi.Modules.PrioritasRiset.Application.GetPrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.Application.GetAllPrioritasRiset
{
    internal sealed class GetAllPrioritasRisetQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllPrioritasRisetQuery, List<PrioritasRisetResponse>>
    {
        public async Task<Result<List<PrioritasRisetResponse>>> Handle(GetAllPrioritasRisetQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama AS Nama
            FROM prioritas_riset
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<PrioritasRisetResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<PrioritasRisetResponse>>(PrioritasRisetErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
