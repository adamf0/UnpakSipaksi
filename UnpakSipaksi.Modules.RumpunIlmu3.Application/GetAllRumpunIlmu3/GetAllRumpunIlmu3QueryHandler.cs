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
using UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.GetRumpunIlmu3;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Application.GetAllRumpunIlmu3
{
    internal sealed class GetAllRumpunIlmu3QueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllRumpunIlmu3Query, List<RumpunIlmu3Response>>
    {
        public async Task<Result<List<RumpunIlmu3Response>>> Handle(GetAllRumpunIlmu3Query request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(`ri3`.uuid, '') AS VARCHAR(36)) AS Uuid,
                `ri3`.nama AS Nama,
                `ri2`.uuid AS UuidRumpunIlmu2
            FROM rumpun_ilmu3 ri3 
            LEFT JOIN rumpun_ilmu2 ri2 on ri3.id_rumpun_ilmu2 = ri2.id
            WHERE `ri3`.uuid = @Uuid
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<RumpunIlmu3Response>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<RumpunIlmu3Response>>(RumpunIlmu3Errors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
