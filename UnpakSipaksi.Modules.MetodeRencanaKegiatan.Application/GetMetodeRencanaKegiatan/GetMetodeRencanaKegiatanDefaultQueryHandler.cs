using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetMetodeRencanaKegiatan
{
    internal sealed class GetMetodeRencanaKegiatanDefaultQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMetodeRencanaKegiatanDefaultQuery, MetodeRencanaKegiatanDefaultResponse>
    {
        public async Task<Result<MetodeRencanaKegiatanDefaultResponse>> Handle(GetMetodeRencanaKegiatanDefaultQuery request, CancellationToken cancellationToken)
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
                 FROM metode_rencana_kegiatan 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<MetodeRencanaKegiatanDefaultResponse?>(sql, new { Uuid = request.MetodeRencanaKegiatanUuid });
            if (result == null)
            {
                return Result.Failure<MetodeRencanaKegiatanDefaultResponse>(MetodeRencanaKegiatanErrors.NotFound(request.MetodeRencanaKegiatanUuid));
            }

            return result;
        }
    }
}
