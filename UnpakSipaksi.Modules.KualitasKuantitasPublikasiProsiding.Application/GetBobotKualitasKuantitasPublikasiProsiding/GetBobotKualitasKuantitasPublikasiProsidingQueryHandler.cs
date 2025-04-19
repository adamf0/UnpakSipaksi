using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetBobotKualitasKuantitasPublikasiProsiding
{
    internal sealed class GetBobotKualitasKuantitasPublikasiProsidingQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotKualitasKuantitasPublikasiProsidingQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotKualitasKuantitasPublikasiProsidingQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM kualitas_kuantitas_publikasi_prosiding
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(KualitasKuantitasPublikasiProsidingErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(KualitasKuantitasPublikasiProsidingErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
