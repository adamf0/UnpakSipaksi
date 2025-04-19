using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Roadmap.Domain.Roadmap;

namespace UnpakSipaksi.Modules.Roadmap.Application.GetRoadmap
{
    internal sealed class GetRoadmapQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRoadmapQuery, RoadmapResponse>
    {
        public async Task<Result<RoadmapResponse>> Handle(GetRoadmapQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     NIDN as Nidn,
                     link as Link
                 FROM roadmap 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<RoadmapResponse?>(sql, new { Uuid = request.RoadmapUuid });
            if (result == null)
            {
                return Result.Failure<RoadmapResponse>(RoadmapErrors.NotFound(request.RoadmapUuid));
            }

            return result;
        }
    }
}
