
using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.GetKesesuaianSolusiMasalahMitra
{
    internal sealed class GetKesesuaianSolusiMasalahMitraDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKesesuaianSolusiMasalahMitraDefaultQuery, KesesuaianSolusiMasalahMitraDefaultResponse>
    {
        public async Task<Result<KesesuaianSolusiMasalahMitraDefaultResponse>> Handle(GetKesesuaianSolusiMasalahMitraDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     nilai AS Nilai 
                 FROM kesesuaian_solusi_masalah_mitra 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KesesuaianSolusiMasalahMitraDefaultResponse?>(sql, new { Uuid = request.KesesuaianSolusiMasalahMitraUuid });
            if (result == null)
            {
                return Result.Failure<KesesuaianSolusiMasalahMitraDefaultResponse>(KesesuaianSolusiMasalahMitraErrors.NotFound(request.KesesuaianSolusiMasalahMitraUuid));
            }

            return result;
        }
    }
}
