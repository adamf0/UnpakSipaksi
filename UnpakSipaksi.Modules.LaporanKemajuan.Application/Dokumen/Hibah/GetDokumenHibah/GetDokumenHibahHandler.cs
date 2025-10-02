using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.LaporanKemajuan.Domain.Dokumen;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Dokumen.Hibah.GetDokumenHibah
{
    internal sealed class GetDokumenHibahHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetDokumenHibahQuery, DokumenHibahResponse>
    {
        public async Task<Result<DokumenHibahResponse>> Handle(GetDokumenHibahQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $"""
                 SELECT 
                     CAST(NULLIF(bp.uuid, '') AS VARCHAR(36)) AS Uuid,
                     CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                     file as File,
                     "Dokumen" as Type
                 FROM berkas_penelitian bp 
                 JOIN penelitian_internal pi ON pi.id = bp.id_pdp
                 WHERE bp.uuid = @Uuid and pi.uuid = @UuidPenelitianHibah

                 UNION ALL

                 SELECT 
                     CAST(NULLIF(pp.uuid, '') AS VARCHAR(36)) AS Uuid,
                     CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                     file as File,
                    "Presentasi" as Type
                 FROM persentasi_penelitian pp 
                 JOIN penelitian_internal pi ON pi.id = pp.id_pdp
                 WHERE pp.uuid = @Uuid
                 """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<DokumenHibahResponse?>(sql, new { Uuid = request.Uuid, UuidPenelitianHibah = request.DokumenHibahUuid });
            if (result == null)
            {
                return Result.Failure<DokumenHibahResponse>(DokumenErrors.NotFound(request.DokumenHibahUuid));
            }

            return result;
        }
    }
}
