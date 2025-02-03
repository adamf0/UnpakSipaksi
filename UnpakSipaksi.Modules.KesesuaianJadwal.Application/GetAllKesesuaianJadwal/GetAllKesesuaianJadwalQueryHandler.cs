using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetKesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetkesesuaianJadwalRepository;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetAllKesesuaianJadwal
{
    internal sealed class GetAllKesesuaianJadwalQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKesesuaianJadwalQuery, List<KesesuaianJadwalResponse>>
    {
        public async Task<Result<List<KesesuaianJadwalResponse>>> Handle(GetAllKesesuaianJadwalQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 nama as Nama,
                 nilai as Nilai
            FROM kesesuaian_jadwal
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KesesuaianJadwalResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KesesuaianJadwalResponse>>(KesesuaianJadwalErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
