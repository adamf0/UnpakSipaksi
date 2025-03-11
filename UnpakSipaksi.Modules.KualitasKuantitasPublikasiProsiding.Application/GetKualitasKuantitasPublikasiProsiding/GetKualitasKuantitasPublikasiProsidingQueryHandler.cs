using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetKualitasKuantitasPublikasiProsiding
{
    internal sealed class GetKualitasKuantitasPublikasiProsidingQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKualitasKuantitasPublikasiProsidingQuery, KualitasKuantitasPublikasiProsidingResponse>
    {
        public async Task<Result<KualitasKuantitasPublikasiProsidingResponse>> Handle(GetKualitasKuantitasPublikasiProsidingQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai as Nilai
                 FROM kualitas_kuantitas_publikasi_prosiding 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KualitasKuantitasPublikasiProsidingResponse?>(sql, new { Uuid = request.KualitasKuantitasPublikasiProsidingUuid });
            if (result == null)
            {
                return Result.Failure<KualitasKuantitasPublikasiProsidingResponse>(KualitasKuantitasPublikasiProsidingErrors.NotFound(request.KualitasKuantitasPublikasiProsidingUuid));
            }

            return result;
        }
    }
}
