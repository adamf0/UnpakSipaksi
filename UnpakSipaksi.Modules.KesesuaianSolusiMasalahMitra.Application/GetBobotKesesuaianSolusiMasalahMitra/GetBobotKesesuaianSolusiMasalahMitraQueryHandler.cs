using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.GetBobotKesesuaianSolusiMasalahMitra
{
    internal sealed class GetBobotKesesuaianSolusiMasalahMitraQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotKesesuaianSolusiMasalahMitraQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotKesesuaianSolusiMasalahMitraQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM kesesuaian_solusi_masalah_mitra
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(KesesuaianSolusiMasalahMitraErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(KesesuaianSolusiMasalahMitraErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
