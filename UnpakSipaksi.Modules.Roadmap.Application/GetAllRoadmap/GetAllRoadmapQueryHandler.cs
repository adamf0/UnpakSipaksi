using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Roadmap.Application.GetRoadmap;
using UnpakSipaksi.Modules.Roadmap.Domain.Roadmap;

namespace UnpakSipaksi.Modules.Roadmap.Application.GetAllRoadmap
{
    internal sealed class GetAllRoadmapQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllRoadmapQuery, List<RoadmapResponse>>
    {
        public async Task<Result<List<RoadmapResponse>>> Handle(GetAllRoadmapQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 NIDN as Nidn,
                 link as Link
            FROM roadmap
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<RoadmapResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<RoadmapResponse>>(RoadmapErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
