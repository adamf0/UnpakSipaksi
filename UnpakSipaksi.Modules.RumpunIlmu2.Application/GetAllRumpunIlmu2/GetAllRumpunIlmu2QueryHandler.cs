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
using UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.GetRumpunIlmu2;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.GetAllRumpunIlmu2
{
    internal sealed class GetAllRumpunIlmu2QueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllRumpunIlmu2Query, List<RumpunIlmu2Response>>
    {
        public async Task<Result<List<RumpunIlmu2Response>>> Handle(GetAllRumpunIlmu2Query request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(`ri2`.uuid, '') AS VARCHAR(36)) AS Uuid,
                `ri2`.nama AS Nama,
                `ri1`.uuid AS UuidRumpunIlmu1
            FROM rumpun_ilmu2 ri2 
            LEFT JOIN rumpun_ilmu1 ri1 on ri2.id_rumpun_ilmu1 = ri1.id
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<RumpunIlmu2Response>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<RumpunIlmu2Response>>(RumpunIlmu2Errors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
