using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Application.GetRumpunIlmu3
{
    internal sealed class GetRumpunIlmu3DefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRumpunIlmu3DefaultQuery, RumpunIlmu3DefaultResponse>
    {
        public async Task<Result<RumpunIlmu3DefaultResponse>> Handle(GetRumpunIlmu3DefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     `ri3`.id as Id,
                     CAST(NULLIF(`ri3`.uuid, '') AS VARCHAR(36)) AS Uuid,
                     `ri3`.nama AS Nama,
                     `ri2`.uuid AS UuidRumpunIlmu2
                 FROM rumpun_ilmu3 ri3 
                 LEFT JOIN rumpun_ilmu2 ri2 on ri3.id_rumpun_ilmu2 = ri2.id
                 WHERE `ri3`.uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<RumpunIlmu3DefaultResponse?>(sql, new { request.Uuid });
            if (result == null)
            {
                return Result.Failure<RumpunIlmu3DefaultResponse>(RumpunIlmu3Errors.NotFound(request.Uuid));
            }

            return result;
        }
    }
}
