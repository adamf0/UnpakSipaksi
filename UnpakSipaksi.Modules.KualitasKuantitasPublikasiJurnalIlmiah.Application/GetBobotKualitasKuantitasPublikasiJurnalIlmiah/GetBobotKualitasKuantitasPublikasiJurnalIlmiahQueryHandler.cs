using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.GetBobotKualitasKuantitasPublikasiJurnalIlmiah
{
    internal sealed class GetBobotKualitasKuantitasPublikasiJurnalIlmiahQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetBobotKualitasKuantitasPublikasiJurnalIlmiahQuery, int?>
    {
        public async Task<Result<int?>> Handle(GetBobotKualitasKuantitasPublikasiJurnalIlmiahQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql = """
                SELECT MAX(nilai)
                FROM kualitas_kuantitas_publikasi_jurnal_ilmiah
            """;

            var nilaiList = (await connection.QueryAsync<int>(sql)).ToList();

            if (!nilaiList.Any())
            {
                return Result.Failure<int?>(KualitasKuantitasPublikasiJurnalIlmiahErrors.EmptyData());
            }

            if (nilaiList.Count > 1)
            {
                return Result.Failure<int?>(KualitasKuantitasPublikasiJurnalIlmiahErrors.NotSameValue());
            }

            return Result.Success(nilaiList?.First());
        }
    }
}
