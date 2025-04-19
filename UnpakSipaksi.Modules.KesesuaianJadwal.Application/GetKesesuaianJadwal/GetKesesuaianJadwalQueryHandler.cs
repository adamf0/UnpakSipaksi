using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetKesesuaianJadwal
{
    internal sealed class GetKesesuaianJadwalQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKesesuaianJadwalQuery, KesesuaianJadwalResponse>
    {
        public async Task<Result<KesesuaianJadwalResponse>> Handle(GetKesesuaianJadwalQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai as Nilai
                 FROM kesesuaian_jadwal 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KesesuaianJadwalResponse?>(sql, new { Uuid = request.KesesuaianJadwalUuid });
            if (result == null)
            {
                return Result.Failure<KesesuaianJadwalResponse>(KesesuaianJadwalErrors.NotFound(request.KesesuaianJadwalUuid));
            }

            return result;
        }
    }
}
