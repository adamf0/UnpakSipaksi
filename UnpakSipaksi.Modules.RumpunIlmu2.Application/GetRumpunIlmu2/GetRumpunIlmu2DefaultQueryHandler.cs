using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.GetRumpunIlmu2
{
    internal sealed class GetRumpunIlmu2DefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRumpunIlmu2DefaultQuery, RumpunIlmu2DefaultResponse>
    {
        public async Task<Result<RumpunIlmu2DefaultResponse>> Handle(GetRumpunIlmu2DefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama  
                 FROM rumpun_ilmu2
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<RumpunIlmu2DefaultResponse?>(sql, new { request.Uuid });
            if (result == null)
            {
                return Result.Failure<RumpunIlmu2DefaultResponse>(RumpunIlmu2Errors.NotFound(request.Uuid));
            }

            return result;
        }
    }
}
