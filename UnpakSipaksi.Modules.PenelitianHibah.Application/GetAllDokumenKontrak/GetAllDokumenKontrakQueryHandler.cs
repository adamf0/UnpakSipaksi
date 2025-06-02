using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetDokumenKontrak;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllDokumenKontrak
{
    internal sealed class GetAllDokumenKontrakQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllDokumenKontrakQuery, List<DokumenKontrakResponse>>
    {
        public async Task<Result<List<DokumenKontrakResponse>>> Handle(GetAllDokumenKontrakQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(dk.uuid, '') AS VARCHAR(36)) AS Uuid,
                CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                dk.file_kontrak AS File
            FROM penelitian_internal_dokumen_kontrak dk 
            LEFT JOIN penelitian_internal pi ON dk.id_pdp = pi.id
            WHERE pi.uuid = @UuidPenelitianHibah
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<DokumenKontrakResponse>(sql, new { UuidPenelitianHibah = request.UuidPenelitianHibah });

            if (result == null || !result.Any())
            {
                return Result.Failure<List<DokumenKontrakResponse>>(PenelitianHibahErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
