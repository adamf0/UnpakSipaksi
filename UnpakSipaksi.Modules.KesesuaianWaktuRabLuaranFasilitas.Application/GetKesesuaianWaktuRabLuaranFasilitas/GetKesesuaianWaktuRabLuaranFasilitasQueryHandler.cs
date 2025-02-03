using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.GetKesesuaianWaktuRabLuaranFasilitas
{
    internal sealed class GetKesesuaianWaktuRabLuaranFasilitasQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKesesuaianWaktuRabLuaranFasilitasQuery, KesesuaianWaktuRabLuaranFasilitasResponse>
    {
        public async Task<Result<KesesuaianWaktuRabLuaranFasilitasResponse>> Handle(GetKesesuaianWaktuRabLuaranFasilitasQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     skor as Skor
                 FROM kesesuaian_waktu_rab_luaran_fasilitas 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KesesuaianWaktuRabLuaranFasilitasResponse?>(sql, new { Uuid = request.KesesuaianWaktuRabLuaranFasilitasUuid });
            if (result == null)
            {
                return Result.Failure<KesesuaianWaktuRabLuaranFasilitasResponse>(KesesuaianWaktuRabLuaranFasilitasErrors.NotFound(request.KesesuaianWaktuRabLuaranFasilitasUuid));
            }

            return result;
        }
    }
}
