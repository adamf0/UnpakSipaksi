using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.GetKualitasKuantitasPublikasiJurnalIlmiah;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.GetAllKualitasKuantitasPublikasiJurnalIlmiah
{
    internal sealed class GetAllKualitasKuantitasPublikasiJurnalIlmiahQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKualitasKuantitasPublikasiJurnalIlmiahQuery, List<KualitasKuantitasPublikasiJurnalIlmiahResponse>>
    {
        public async Task<Result<List<KualitasKuantitasPublikasiJurnalIlmiahResponse>>> Handle(GetAllKualitasKuantitasPublikasiJurnalIlmiahQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 nama as Nama,
                 nilai as Nilai
            FROM kualitas_kuantitas_publikasi_jurnal_ilmiah
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KualitasKuantitasPublikasiJurnalIlmiahResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KualitasKuantitasPublikasiJurnalIlmiahResponse>>(KualitasKuantitasPublikasiJurnalIlmiahErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
