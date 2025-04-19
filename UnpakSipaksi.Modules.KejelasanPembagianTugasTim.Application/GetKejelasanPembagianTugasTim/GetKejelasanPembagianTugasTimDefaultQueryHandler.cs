using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.GetKejelasanPembagianTugasTim
{
    internal sealed class GetKejelasanPembagianTugasTimDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKejelasanPembagianTugasTimDefaultQuery, KejelasanPembagianTugasTimDefaultResponse>
    {
        public async Task<Result<KejelasanPembagianTugasTimDefaultResponse>> Handle(GetKejelasanPembagianTugasTimDefaultQuery request, CancellationToken cancellationToken)
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
                     skor as Skor
                 FROM kejelasan_pembagian_tugas_tim
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KejelasanPembagianTugasTimDefaultResponse?>(sql, new { Uuid = request.KejelasanPembagianTugasTimUuid });
            if (result == null)
            {
                return Result.Failure<KejelasanPembagianTugasTimDefaultResponse>(KejelasanPembagianTugasTimErrors.NotFound(request.KejelasanPembagianTugasTimUuid));
            }

            return result;
        }
    }
}
