using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Application.GetBobotKesesuaianJadwal
{
    internal sealed class GetBobotKesesuaianJadwalQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotKesesuaianJadwalQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotKesesuaianJadwalQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM kesesuaian_jadwal
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(KesesuaianJadwalErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(KesesuaianJadwalErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
