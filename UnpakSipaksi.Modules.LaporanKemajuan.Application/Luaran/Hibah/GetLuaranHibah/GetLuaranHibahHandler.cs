using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.LaporanKemajuan.Application.Luaran.Hibah.GetLuaranHibah;
using UnpakSipaksi.Modules.LaporanKemajuan.Domain.Luaran;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Luaran.Hibah.GetLuaranHibah
{
    internal sealed class GetLuaranHibahHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetLuaranHibahQuery, LuaranHibahResponse>
    {
        public async Task<Result<LuaranHibahResponse>> Handle(GetLuaranHibahQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(bp.uuid, '') AS VARCHAR(36)) AS Uuid,
                     CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                     file as File,
                     "Luaran" as Type
                 FROM berkas_penelitian bp 
                 JOIN penelitian_internal pi ON pi.id = bp.id_pdp
                 WHERE bp.uuid = @Uuid and pi.uuid = @UuidPenelitianHibah

                 UNION ALL

                 SELECT 
                     CAST(NULLIF(pp.uuid, '') AS VARCHAR(36)) AS Uuid,
                     CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                     file as File,
                    "Presentasi" as Type
                 FROM persentasi_penelitian pp 
                 JOIN penelitian_internal pi ON pi.id = pp.id_pdp
                 WHERE pp.uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<LuaranHibahResponse?>(sql, new { Uuid = request.Uuid, UuidPenelitianHibah = request.LuaranUuid });
            if (result == null)
            {
                return Result.Failure<LuaranHibahResponse>(LuaranErrors.NotFound(request.LuaranUuid));
            }

            return result;
        }
    }
}
