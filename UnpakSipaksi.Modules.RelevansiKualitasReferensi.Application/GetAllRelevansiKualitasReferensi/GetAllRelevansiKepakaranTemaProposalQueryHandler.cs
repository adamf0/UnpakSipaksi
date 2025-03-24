using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetRelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.GetAllRelevansiKualitasReferensi
{
    internal sealed class GetAllRelevansiKualitasReferensiQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllRelevansiKualitasReferensiQuery, List<RelevansiKualitasReferensiResponse>>
    {
        public async Task<Result<List<RelevansiKualitasReferensiResponse>>> Handle(GetAllRelevansiKualitasReferensiQuery request, CancellationToken cancellationToken)
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
            FROM relevansi_kualitas_referensi
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<RelevansiKualitasReferensiResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<RelevansiKualitasReferensiResponse>>(RelevansiKualitasReferensiErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
