using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.GetSotaKebaharuan;
using UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.GetAllSotaKebaharuan
{
    internal sealed class GetAllSotaKebaharuanQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllSotaKebaharuanQuery, List<SotaKebaharuanResponse>>
    {
        public async Task<Result<List<SotaKebaharuanResponse>>> Handle(GetAllSotaKebaharuanQuery request, CancellationToken cancellationToken)
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
            FROM sota_kebaharuan
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<SotaKebaharuanResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<SotaKebaharuanResponse>>(SotaKebaharuanErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
