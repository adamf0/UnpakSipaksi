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
using UnpakSipaksi.Modules.FokusPengabdian.Application.GetFokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian;

namespace UnpakSipaksi.Modules.FokusPengabdian.Application.GetAllFokusPengabdian
{
    internal sealed class GetAllFokusPengabdianQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllFokusPengabdianQuery, List<FokusPengabdianResponse>>
    {
        public async Task<Result<List<FokusPengabdianResponse>>> Handle(GetAllFokusPengabdianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                name as Nama 
            FROM fokus_pengabdian 
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<FokusPengabdianResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<FokusPengabdianResponse>>(FokusPengabdianErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
