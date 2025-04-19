using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.GetBobotKualitasIpteks
{
    internal sealed class GetBobotKualitasIpteksQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotKualitasIpteksQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotKualitasIpteksQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM kualitas_ipteks
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(KualitasIpteksErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(KualitasIpteksErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
