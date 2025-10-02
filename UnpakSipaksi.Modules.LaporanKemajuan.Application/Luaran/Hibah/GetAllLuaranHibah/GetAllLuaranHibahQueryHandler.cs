using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.LaporanKemajuan.Application.Luaran.Hibah.GetLuaranHibah;
using UnpakSipaksi.Modules.LaporanKemajuan.Domain.Luaran;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Luaran.Hibah.GetAllDokumenHibah
{
    internal sealed class GetAllLuaranHibahQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllLuaranHibahQuery, List<LuaranHibahResponse>>
    {
        public async Task<Result<List<LuaranHibahResponse>>> Handle(GetAllLuaranHibahQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(bp.uuid, '') AS VARCHAR(36)) AS Uuid,
                CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                file as File,
                "Dokumen" as Type
            FROM berkas_penelitian bp 
            JOIN penelitian_internal pi ON pi.id = bp.id_pdp
            WHERE pi.uuid = @UuidPenelitianHibah
            
            UNION ALL
            
            SELECT 
                CAST(NULLIF(pp.uuid, '') AS VARCHAR(36)) AS Uuid,
                CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                file as File,
               "Presentasi" as Type
            FROM persentasi_penelitian pp 
            JOIN penelitian_internal pi ON pi.id = pp.id_pdp
            WHERE pi.uuid = @UuidPenelitianHibah
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<LuaranHibahResponse>(sql, new { UuidPenelitianHibah = request.UuidPenelitianHibah });

            if (result == null || !result.Any())
            {
                return Result.Failure<List<LuaranHibahResponse>>(LuaranErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
