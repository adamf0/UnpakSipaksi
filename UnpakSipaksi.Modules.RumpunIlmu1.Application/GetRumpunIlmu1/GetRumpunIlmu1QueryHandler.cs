using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Application.GetRumpunIlmu1
{
    internal sealed class GetRumpunIlmu1QueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRumpunIlmu1Query, RumpunIlmu1Response>
    {
        public async Task<Result<RumpunIlmu1Response>> Handle(GetRumpunIlmu1Query request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama AS Nama
                 FROM rumpun_ilmu1
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<RumpunIlmu1Response?>(sql, new { request.Uuid });
            if (result == null)
            {
                return Result.Failure<RumpunIlmu1Response>(RumpunIlmu1Errors.NotFound(request.Uuid));
            }

            return result;
        }
    }
}
