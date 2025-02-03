using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.GetKesesuaianSolusiMasalahMitra
{
    internal sealed class GetKesesuaianSolusiMasalahMitraQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKesesuaianSolusiMasalahMitraQuery, KesesuaianSolusiMasalahMitraResponse>
    {
        public async Task<Result<KesesuaianSolusiMasalahMitraResponse>> Handle(GetKesesuaianSolusiMasalahMitraQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai as Nilai
                 FROM kesesuaian_solusi_masalah_mitra 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KesesuaianSolusiMasalahMitraResponse?>(sql, new { Uuid = request.KesesuaianSolusiMasalahMitraUuid });
            if (result == null)
            {
                return Result.Failure<KesesuaianSolusiMasalahMitraResponse>(KesesuaianSolusiMasalahMitraErrors.NotFound(request.KesesuaianSolusiMasalahMitraUuid));
            }

            return result;
        }
    }
}
