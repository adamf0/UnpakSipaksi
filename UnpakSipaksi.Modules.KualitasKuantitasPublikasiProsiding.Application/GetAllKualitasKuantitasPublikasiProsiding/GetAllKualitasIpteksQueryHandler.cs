using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetKualitasKuantitasPublikasiProsiding;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetAllKualitasKuantitasPublikasiProsiding
{
    internal sealed class GetAllKualitasKuantitasPublikasiProsidingQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKualitasKuantitasPublikasiProsidingQuery, List<KualitasKuantitasPublikasiProsidingResponse>>
    {
        public async Task<Result<List<KualitasKuantitasPublikasiProsidingResponse>>> Handle(GetAllKualitasKuantitasPublikasiProsidingQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 nama as Nama,
                 nilai as Nilai
            FROM kualitas_kuantitas_publikasi_prosiding
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KualitasKuantitasPublikasiProsidingResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KualitasKuantitasPublikasiProsidingResponse>>(KualitasKuantitasPublikasiProsidingErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
