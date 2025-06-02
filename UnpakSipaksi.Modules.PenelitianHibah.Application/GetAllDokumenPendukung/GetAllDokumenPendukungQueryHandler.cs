using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetDokumenPendukung;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllDokumenPendukung
{
    internal sealed class GetAllDokumenPendukungQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllDokumenPendukungQuery, List<DokumenPendukungResponse>>
    {
        public async Task<Result<List<DokumenPendukungResponse>>> Handle(GetAllDokumenPendukungQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(dp.uuid, '') AS VARCHAR(36)) AS Uuid,
                CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                dp.file_mitra AS File,
                dp.Link AS Link,
                dp.kategori AS Kategori
            FROM penelitian_internal_dokumen_pendukung dp 
            LEFT JOIN penelitian_internal pi ON dp.id_pdp = pi.id
            WHERE pi.uuid = @UuidPenelitianHibah
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<DokumenPendukungResponse>(sql, new { UuidPenelitianHibah = request.UuidPenelitianHibah });

            if (result == null || !result.Any())
            {
                return Result.Failure<List<DokumenPendukungResponse>>(PenelitianHibahErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
