using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetBobotKewajaranTahapanTarget
{
    internal sealed class GetBobotKewajaranTahapanTargetQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotKewajaranTahapanTargetQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotKewajaranTahapanTargetQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM kewajaran_tahapan_target
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(KewajaranTahapanTargetErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(KewajaranTahapanTargetErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
