using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Application.GetBobotKesesuaianPenugasan
{
    internal sealed class GetBobotKesesuaianPenugasanQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotKesesuaianPenugasanQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotKesesuaianPenugasanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM kesesuaian_penugasan
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(KesesuaianPenugasanErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(KesesuaianPenugasanErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
