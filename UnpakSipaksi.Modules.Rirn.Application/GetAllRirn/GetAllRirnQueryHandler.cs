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
using UnpakSipaksi.Modules.Rirn.Domain.Rirn;
using UnpakSipaksi.Modules.Rirn.Application.GetRirn;

namespace UnpakSipaksi.Modules.Rirn.Application.GetAllRirn
{
    internal sealed class GetAllRirnQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllRirnQuery, List<RirnResponse>>
    {
        public async Task<Result<List<RirnResponse>>> Handle(GetAllRirnQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama AS Nama
            FROM rirn
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<RirnResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<RirnResponse>>(RirnErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
