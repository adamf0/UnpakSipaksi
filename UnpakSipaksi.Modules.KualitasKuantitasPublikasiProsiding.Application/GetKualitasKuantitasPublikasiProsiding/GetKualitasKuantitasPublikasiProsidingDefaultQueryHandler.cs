using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetKualitasKuantitasPublikasiProsiding
{
    internal sealed class GetKualitasKuantitasPublikasiProsidingDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetKualitasKuantitasPublikasiProsidingDefaultQuery, KualitasKuantitasPublikasiProsidingDefaultResponse>
    {
        public async Task<Result<KualitasKuantitasPublikasiProsidingDefaultResponse>> Handle(GetKualitasKuantitasPublikasiProsidingDefaultQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     id as Id,
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     name as Nama,
                     nilai AS Nilai 
                 FROM kualitas_kuantitas_publikasi_prosiding 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<KualitasKuantitasPublikasiProsidingDefaultResponse?>(sql, new { Uuid = request.KualitasKuantitasPublikasiProsidingUuid });
            if (result == null)
            {
                return Result.Failure<KualitasKuantitasPublikasiProsidingDefaultResponse>(KualitasKuantitasPublikasiProsidingErrors.NotFound(request.KualitasKuantitasPublikasiProsidingUuid));
            }

            return result;
        }
    }
}
