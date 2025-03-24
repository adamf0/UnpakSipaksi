using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.GetRumpunIlmu2
{
    internal sealed class GetRumpunIlmu2QueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRumpunIlmu2Query, RumpunIlmu2Response>
    {
        public async Task<Result<RumpunIlmu2Response>> Handle(GetRumpunIlmu2Query request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(`ri2`.uuid, '') AS VARCHAR(36)) AS Uuid,
                     `ri2`.nama AS Nama,
                     `ri1`.uuid AS UuidRumpunIlmu1
                 FROM rumpun_ilmu2 ri2 
                 LEFT JOIN rumpun_ilmu1 ri1 on ri2.id_rumpun_ilmu1 = ri1.id
                 WHERE `ri2`.uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<RumpunIlmu2Response?>(sql, new { request.Uuid });
            if (result == null)
            {
                return Result.Failure<RumpunIlmu2Response>(RumpunIlmu2Errors.NotFound(request.Uuid));
            }

            return result;
        }
    }
}
