using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetBobotKuantitasStatusKi
{
    internal sealed class GetBobotKuantitasStatusKiQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotKuantitasStatusKiQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotKuantitasStatusKiQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM kuantitas_status_ki
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(KuantitasStatusKiErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(KuantitasStatusKiErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
