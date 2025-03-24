using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.Application.GetPrioritasRiset
{
    internal sealed class GetPrioritasRisetQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPrioritasRisetQuery, PrioritasRisetResponse>
    {
        public async Task<Result<PrioritasRisetResponse>> Handle(GetPrioritasRisetQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama AS Nama
                 FROM prioritas_riset 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<PrioritasRisetResponse?>(sql, new { Uuid = request.Uuid });
            if (result == null)
            {
                return Result.Failure<PrioritasRisetResponse>(PrioritasRisetErrors.NotFound(request.Uuid));
            }

            return result;
        }
    }
}
