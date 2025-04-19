using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetRoadmapPenelitian
{
    internal sealed class GetRoadmapPenelitianQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRoadmapPenelitianQuery, RoadmapPenelitianResponse>
    {
        public async Task<Result<RoadmapPenelitianResponse>> Handle(GetRoadmapPenelitianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     skor AS Skor 
                 FROM roadmap_penelitian 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<RoadmapPenelitianResponse?>(sql, new { Uuid = request.RoadmapPenelitianUuid });
            if (result == null)
            {
                return Result.Failure<RoadmapPenelitianResponse>(RoadmapPenelitianErrors.NotFound(request.RoadmapPenelitianUuid));
            }

            return result;
        }
    }
}
