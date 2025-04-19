using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.GetKesesuaianWaktuRabLuaranFasilitas
{
    internal sealed class GetKesesuaianWaktuRabLuaranFasilitasDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKesesuaianWaktuRabLuaranFasilitasDefaultQuery, KesesuaianWaktuRabLuaranFasilitasDefaultResponse>
    {
        public async Task<Result<KesesuaianWaktuRabLuaranFasilitasDefaultResponse>> Handle(GetKesesuaianWaktuRabLuaranFasilitasDefaultQuery request, CancellationToken cancellationToken)
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
                 FROM kesesuaian_waktu_rab_luaran_fasilitas 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KesesuaianWaktuRabLuaranFasilitasDefaultResponse?>(sql, new { Uuid = request.KesesuaianWaktuRabLuaranFasilitasUuid });
            if (result == null)
            {
                return Result.Failure<KesesuaianWaktuRabLuaranFasilitasDefaultResponse>(KesesuaianWaktuRabLuaranFasilitasErrors.NotFound(request.KesesuaianWaktuRabLuaranFasilitasUuid));
            }

            return result;
        }
    }
}
