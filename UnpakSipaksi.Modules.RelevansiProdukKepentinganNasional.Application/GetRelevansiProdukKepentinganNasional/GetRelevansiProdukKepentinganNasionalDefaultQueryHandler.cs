using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetRelevansiProdukKepentinganNasional
{
    internal sealed class GetRelevansiProdukKepentinganNasionalDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRelevansiProdukKepentinganNasionalDefaultQuery, RelevansiProdukKepentinganNasionalDefaultResponse>
    {
        public async Task<Result<RelevansiProdukKepentinganNasionalDefaultResponse>> Handle(GetRelevansiProdukKepentinganNasionalDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     bobot_pdp AS BobotPDP,
                     bobot_terapan AS BobotTerapan,
                     bobot_kerjasama AS BobotKerjasama,
                     bobot_penelitian_dasar AS BobotPenelitianDasar,
                     skor AS Skor 
                 FROM relevansi_produk_kepentingan_nasional 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<RelevansiProdukKepentinganNasionalDefaultResponse?>(sql, new { request.Uuid });
            if (result == null)
            {
                return Result.Failure<RelevansiProdukKepentinganNasionalDefaultResponse>(RelevansiProdukKepentinganNasionalErrors.NotFound(request.Uuid));
            }

            return result;
        }
    }
}
