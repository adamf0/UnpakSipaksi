using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.GetVideoKegiatan
{
    internal sealed class GetVideoKegiatanQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetVideoKegiatanQuery, VideoKegiatanResponse>
    {
        public async Task<Result<VideoKegiatanResponse>> Handle(GetVideoKegiatanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai as Nilai
                 FROM video_kegiatan 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<VideoKegiatanResponse?>(sql, new { Uuid = request.VideoKegiatanUuid });
            if (result == null)
            {
                return Result.Failure<VideoKegiatanResponse>(VideoKegiatanErrors.NotFound(request.VideoKegiatanUuid));
            }

            return result;
        }
    }
}
