using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.GetVideoKegiatan
{
    internal sealed class GetVideoKegiatanDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetVideoKegiatanDefaultQuery, VideoKegiatanDefaultResponse>
    {
        public async Task<Result<VideoKegiatanDefaultResponse>> Handle(GetVideoKegiatanDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     nilai AS Nilai 
                 FROM video_kegiatan 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<VideoKegiatanDefaultResponse?>(sql, new { Uuid = request.VideoKegiatanUuid });
            if (result == null)
            {
                return Result.Failure<VideoKegiatanDefaultResponse>(VideoKegiatanErrors.NotFound(request.VideoKegiatanUuid));
            }

            return result;
        }
    }
}
