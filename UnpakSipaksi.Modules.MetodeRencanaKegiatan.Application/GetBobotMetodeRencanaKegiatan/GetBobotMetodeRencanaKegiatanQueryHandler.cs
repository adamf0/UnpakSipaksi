using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetBobotMetodeRencanaKegiatan
{
    internal sealed class GetBobotMetodeRencanaKegiatanQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotMetodeRencanaKegiatanQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotMetodeRencanaKegiatanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM metode_rencana_kegiatan
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(MetodeRencanaKegiatanErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(MetodeRencanaKegiatanErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
