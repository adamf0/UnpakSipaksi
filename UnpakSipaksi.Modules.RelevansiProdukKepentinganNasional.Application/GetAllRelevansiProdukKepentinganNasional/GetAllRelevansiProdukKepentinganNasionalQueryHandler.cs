using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetRelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetAllRelevansiProdukKepentinganNasional
{
    internal sealed class GetAllRelevansiProdukKepentinganNasionalQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllRelevansiProdukKepentinganNasionalQuery, List<RelevansiProdukKepentinganNasionalResponse>>
    {
        public async Task<Result<List<RelevansiProdukKepentinganNasionalResponse>>> Handle(GetAllRelevansiProdukKepentinganNasionalQuery request, CancellationToken cancellationToken)
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
            FROM relevansi_produk_kepentingan_nasional
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<RelevansiProdukKepentinganNasionalResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<RelevansiProdukKepentinganNasionalResponse>>(RelevansiProdukKepentinganNasionalErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
