using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetKesesuaianJadwal
{
    internal sealed class GetKesesuaianJadwalDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKesesuaianJadwalDefaultQuery, KesesuaianJadwalDefaultResponse>
    {
        public async Task<Result<KesesuaianJadwalDefaultResponse>> Handle(GetKesesuaianJadwalDefaultQuery request, CancellationToken cancellationToken)
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
                 FROM kesesuaian_jadwal 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KesesuaianJadwalDefaultResponse?>(sql, new { Uuid = request.KesesuaianJadwalUuid });
            if (result == null)
            {
                return Result.Failure<KesesuaianJadwalDefaultResponse>(KesesuaianJadwalErrors.NotFound(request.KesesuaianJadwalUuid));
            }

            return result;
        }
    }
}
