using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.VideoKegiatan.Application.GetVideoKegiatan;
using UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.GetAllVideoKegiatan
{
    internal sealed class GetAllVideoKegiatanQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllVideoKegiatanQuery, List<VideoKegiatanResponse>>
    {
        public async Task<Result<List<VideoKegiatanResponse>>> Handle(GetAllVideoKegiatanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 nama as Nama,
                 nilai as Nilai
            FROM video_kegiatan
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<VideoKegiatanResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<VideoKegiatanResponse>>(VideoKegiatanErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
