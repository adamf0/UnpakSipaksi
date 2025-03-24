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
using UnpakSipaksi.Modules.RumpunIlmu1.Application.GetRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Application.GetAllRumpunIlmu1
{
    internal sealed class GetAllRumpunIlmu1QueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllRumpunIlmu1Query, List<RumpunIlmu1Response>>
    {
        public async Task<Result<List<RumpunIlmu1Response>>> Handle(GetAllRumpunIlmu1Query request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                nama AS Nama
            FROM rumpun_ilmu1
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<RumpunIlmu1Response>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<RumpunIlmu1Response>>(RumpunIlmu1Errors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
