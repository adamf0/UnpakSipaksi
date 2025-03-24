using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetRoadmapPenelitian;
using UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetAllRoadmapPenelitian
{
    internal sealed class GetAllRoadmapPenelitianQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllRoadmapPenelitianQuery, List<RoadmapPenelitianResponse>>
    {
        public async Task<Result<List<RoadmapPenelitianResponse>>> Handle(GetAllRoadmapPenelitianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                name as Nama,
                bobot_pdp AS BobotPDP,
                bobot_terapan AS BobotTerapan,
                bobot_kerjasama AS BobotKerjasama,
                bobot_penelitian_dasar AS BobotPenelitianDasar,
                skor AS Skor 
            FROM roadmap_penelitian
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<RoadmapPenelitianResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<RoadmapPenelitianResponse>>(RoadmapPenelitianErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
