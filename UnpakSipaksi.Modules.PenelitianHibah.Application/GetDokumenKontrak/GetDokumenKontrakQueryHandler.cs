using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenKontrak;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetDokumenKontrak
{
    internal sealed class GetDokumenKontrakQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetDokumenKontrakQuery, DokumenKontrakResponse>
    {
        public async Task<Result<DokumenKontrakResponse>> Handle(GetDokumenKontrakQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $""""
                SELECT 
                    CAST(NULLIF(dk.uuid, '') AS VARCHAR(36)) AS Uuid,
                    CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                    dk.file_kontrak AS File
                FROM penelitian_internal_dokumen_kontrak dk 
                LEFT JOIN penelitian_internal pi ON dk.id_pdp = pi.id
                WHERE dk.uuid = @Uuid and pi.uuid = @UuidPenelitianHibah
                """";

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<DokumenKontrakResponse>(sql, new { Uuid = request.Uuid, UuidPenelitianHibah = request.UuidPenelitianHibah });
            if (result == null)
            {
                return Result.Failure<DokumenKontrakResponse>(DokumenKontrakErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            return result;
        }
    }
}
