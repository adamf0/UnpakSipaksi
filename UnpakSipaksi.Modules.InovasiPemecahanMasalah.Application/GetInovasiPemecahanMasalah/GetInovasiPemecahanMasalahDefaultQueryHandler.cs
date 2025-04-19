using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.GetInovasiPemecahanMasalah
{
    internal sealed class GetInovasiPemecahanMasalahDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetInovasiPemecahanMasalahDefaultQuery, InovasiPemecahanMasalahDefaultResponse>
    {
        public async Task<Result<InovasiPemecahanMasalahDefaultResponse>> Handle(GetInovasiPemecahanMasalahDefaultQuery request, CancellationToken cancellationToken)
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
                 FROM inovasi_pemecahan_masalah 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<InovasiPemecahanMasalahDefaultResponse?>(sql, new { Uuid = request.InovasiPemecahanMasalahUuid });
            if (result == null)
            {
                return Result.Failure<InovasiPemecahanMasalahDefaultResponse>(InovasiPemecahanMasalahErrors.NotFound(request.InovasiPemecahanMasalahUuid));
            }

            return result;
        }
    }
}
