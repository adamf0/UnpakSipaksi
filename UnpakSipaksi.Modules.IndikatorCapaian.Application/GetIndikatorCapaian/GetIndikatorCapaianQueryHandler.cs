using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.IndikatorCapaian.Domain;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Application.GetIndikatorCapaian
{
    internal sealed class GetIndikatorCapaianQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetIndikatorCapaianQuery, IndikatorCapaianResponse>
    {
        public async Task<Result<IndikatorCapaianResponse>> Handle(GetIndikatorCapaianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(ic.uuid, '') AS VARCHAR(36)) AS Uuid,
                     CAST(NULLIF(jl.uuid, '') AS VARCHAR(36)) AS UuidJenisLuaran,
                     ic.nama as Nama,
                     ic.status as Status
                 FROM pkm_indikator_capaian ic 
                 LEFT JOIN pkm_jenis_luaran jl ON ic.id_jenis_luaran = jl.id
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<IndikatorCapaianResponse?>(sql, new { Uuid = request.IndikatorCapaianUuid });
            if (result == null)
            {
                return Result.Failure<IndikatorCapaianResponse>(IndikatorCapaianErrors.NotFound(request.IndikatorCapaianUuid));
            }

            return result;
        }
    }
}
