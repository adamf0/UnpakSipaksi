using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt;

namespace UnpakSipaksi.Modules.KategoriTkt.Application.GetKategoriTkt
{
    internal sealed class GetKategoriTktDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKategoriTktDefaultQuery, KategoriTktDefaultResponse>
    {
        public async Task<Result<KategoriTktDefaultResponse>> Handle(GetKategoriTktDefaultQuery request, CancellationToken cancellationToken)
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
                 FROM kategori_tkt 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KategoriTktDefaultResponse?>(sql, new { Uuid = request.KategoriTktUuid });
            if (result == null)
            {
                return Result.Failure<KategoriTktDefaultResponse>(KategoriTktErrors.NotFound(request.KategoriTktUuid));
            }

            return result;
        }
    }
}
