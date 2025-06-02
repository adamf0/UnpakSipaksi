using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenPendukung;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetDokumenPendukung
{
    internal sealed class GetDokumenPendukungQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetDokumenPendukungQuery, DokumenPendukungResponse>
    {
        public async Task<Result<DokumenPendukungResponse>> Handle(GetDokumenPendukungQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $""""
                SELECT 
                    CAST(NULLIF(dp.uuid, '') AS VARCHAR(36)) AS Uuid,
                    CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                    dp.file_mitra AS File,
                    dp.Link AS Link,
                    dp.kategori AS Kategori
                FROM penelitian_internal_dokumen_pendukung dp 
                LEFT JOIN penelitian_internal pi ON dp.id_pdp = pi.id
                WHERE dp.uuid = @Uuid and pi.uuid = @UuidPenelitianHibah
                """";

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<DokumenPendukungResponse>(sql, new { Uuid = request.Uuid, UuidPenelitianHibah = request.UuidPenelitianHibah });
            if (result == null)
            {
                return Result.Failure<DokumenPendukungResponse>(DokumenPendukungErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            return result;
        }
    }
}
