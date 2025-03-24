using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetMetodeRencanaKegiatan
{
    internal sealed class GetMetodeRencanaKegiatanQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMetodeRencanaKegiatanQuery, MetodeRencanaKegiatanResponse>
    {
        public async Task<Result<MetodeRencanaKegiatanResponse>> Handle(GetMetodeRencanaKegiatanQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                     nama as Nama,
                     nilai as Nilai
                 FROM metode_rencana_kegiatan 
                 WHERE uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<MetodeRencanaKegiatanResponse?>(sql, new { Uuid = request.MetodeRencanaKegiatanUuid });
            if (result == null)
            {
                return Result.Failure<MetodeRencanaKegiatanResponse>(MetodeRencanaKegiatanErrors.NotFound(request.MetodeRencanaKegiatanUuid));
            }

            return result;
        }
    }
}
