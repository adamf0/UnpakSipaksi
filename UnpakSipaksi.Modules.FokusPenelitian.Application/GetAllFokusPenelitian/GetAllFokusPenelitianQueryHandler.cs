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
using UnpakSipaksi.Modules.FokusPenelitian.Application.GetAllFokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Application.GetFokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian;

namespace UnpakSipaksi.Modules.FokusPenelitian.Application.GetAllFokusPenelitian
{
    internal sealed class GetAllFokusPenelitianQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllFokusPenelitianQuery, List<FokusPenelitianResponse>>
    {
        public async Task<Result<List<FokusPenelitianResponse>>> Handle(GetAllFokusPenelitianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama as Nama  
            FROM bidang_fokus_penelitian
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<FokusPenelitianResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<FokusPenelitianResponse>>(FokusPenelitianErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
