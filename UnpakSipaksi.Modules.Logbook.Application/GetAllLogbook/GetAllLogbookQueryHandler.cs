using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Logbook.Domain.Logbook;
using UnpakSipaksi.Modules.Logbook.Application.GetLogbook;

namespace UnpakSipaksi.Modules.Logbook.Application.GetAllLogbook
{
    internal sealed class GetAllLogbookQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllLogbookQuery, List<LogbookResponse>>
    {
        public async Task<Result<List<LogbookResponse>>> Handle(GetAllLogbookQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
                 """
                SELECT 
                    CAST(NULLIF(l.uuid, '') AS CHAR(36)) AS Uuid,
                    CAST(NULLIF(pi.uuid, '') AS CHAR(36)) AS UuidPenelitianHibah,
                    CAST(NULLIF(p.uuid, '') AS CHAR(36)) AS UuidPenelitianPkm,
                    CASE(
                        WHEN pi.uuid IS NOT NULL AND p.uuid IS NULL THEN pi.NIDN 
                        WHEN pi.uuid IS NULL AND p.uuid IS NOT NULL THEN p.NIDN 
                        ELSE null
                    ) as NIDN,
                    l.tanggal_kegiatan AS TanggalKegiatan,
                    l.lampiran AS Lampiran,
                    l.deskripsi AS Deskripsi,
                    l.persentase AS Persentase
                FROM logbook l
                LEFT JOIN (
                    SELECT id, uuid, NIDN FROM penelitian_internal
                ) pi ON l.id_pdp = pi.id
                LEFT JOIN (
                    SELECT id, uuid, NIDN FROM pkm
                ) p ON l.id_pkm = p.id
                WHERE 
                    (
                        (
                            (@UuidPenelitianHibah IS NOT NULL AND pi.uuid = @UuidPenelitianHibah) AND 
                            (@NIDN IS NOT NULL AND pi.NIDN = @NIDN)
                        ) OR
                        (
                            (@UuidPenelitianPkm IS NOT NULL AND p.uuid = @UuidPenelitianPkm) AND 
                            (@NIDN IS NOT NULL AND p.NIDN = @NIDN)
                        )
                    )
                """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<LogbookResponse>(sql, new
            {
                NIDN = request.NIDN,
                UuidPenelitianHibah = request.UudPenelitianHibah,
                UuidPenelitianPkm = request.UudPenelitianPkm
            });

            if (result == null || !result.Any())
            {
                return Result.Failure<List<LogbookResponse>>(LogbookErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
