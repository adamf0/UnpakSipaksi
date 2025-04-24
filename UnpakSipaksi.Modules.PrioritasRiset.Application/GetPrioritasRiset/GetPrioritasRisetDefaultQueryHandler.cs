using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.Application.GetPrioritasRiset
{
    internal sealed class GetPrioritasRisetDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPrioritasRisetDefaultQuery, PrioritasRisetDefaultResponse>
    {
        public async Task<Result<PrioritasRisetDefaultResponse>> Handle(GetPrioritasRisetDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama 
                 FROM prioritas_riset 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<PrioritasRisetDefaultResponse?>(sql, new { Uuid = request.PrioritasRisetUuid });
            if (result == null)
            {
                return Result.Failure<PrioritasRisetDefaultResponse>(PrioritasRisetErrors.NotFound(request.PrioritasRisetUuid));
            }

            return result;
        }
    }
}
