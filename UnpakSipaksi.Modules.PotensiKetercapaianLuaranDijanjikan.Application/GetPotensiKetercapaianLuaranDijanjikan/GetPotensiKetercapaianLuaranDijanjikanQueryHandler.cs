using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.GetPotensiKetercapaianLuaranDijanjikan
{
    internal sealed class GetPotensiKetercapaianLuaranDijanjikanQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPotensiKetercapaianLuaranDijanjikanQuery, PotensiKetercapaianLuaranDijanjikanResponse>
    {
        public async Task<Result<PotensiKetercapaianLuaranDijanjikanResponse>> Handle(GetPotensiKetercapaianLuaranDijanjikanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     bobot_pdp AS BobotPDP,
                     bobot_terapan AS BobotTerapan,
                     bobot_kerjasama AS BobotKerjasama,
                     bobot_penelitian_dasar AS BobotPenelitianDasar,
                     skor AS Skor 
                 FROM potensi_ketercapaian_luaran_dijanjikan 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<PotensiKetercapaianLuaranDijanjikanResponse?>(sql, new { Uuid = request.PotensiKetercapaianLuaranDijanjikanUuid });
            if (result == null)
            {
                return Result.Failure<PotensiKetercapaianLuaranDijanjikanResponse>(PotensiKetercapaianLuaranDijanjikanErrors.NotFound(request.PotensiKetercapaianLuaranDijanjikanUuid));
            }

            return result;
        }
    }
}
