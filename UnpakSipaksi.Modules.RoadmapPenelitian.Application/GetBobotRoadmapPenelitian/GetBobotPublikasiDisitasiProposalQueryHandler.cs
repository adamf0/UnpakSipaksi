using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetBobotRoadmapPenelitian
{
    internal sealed class GetBobotRoadmapPenelitianQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotRoadmapPenelitianQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotRoadmapPenelitianQuery request, CancellationToken cancellationToken)
        {
            string column = request.KategoriSkema switch
            {
                "Penelitian Dasar" => "bobot_pdp",
                "Penelitian Dosen Pemula (PDP)" => "bobot_penelitian_dasar",
                "Penelitian Terapan" => "bobot_terapan",
                "Penelitian Kolaborasi" => "bobot_kerjasama",
                _ => string.Empty,
            };
            if (string.IsNullOrEmpty(column))
            {
                return Result.Failure<int?>(RoadmapPenelitianErrors.UnknownKategoriSkema());
            }

            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            var sql = $"""
                SELECT DISTINCT {column}
                FROM roadmap_penelitian
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(RoadmapPenelitianErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(RoadmapPenelitianErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
