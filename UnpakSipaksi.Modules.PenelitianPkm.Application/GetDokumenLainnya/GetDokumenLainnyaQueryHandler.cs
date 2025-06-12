using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenLainnya;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenLainnya;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetDokumenLainnya
{
    internal sealed class GetDokumenLainnyaQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetDokumenLainnyaQuery, DokumenLainnyaResponse>
    {
        public async Task<Result<DokumenLainnyaResponse>> Handle(GetDokumenLainnyaQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $""""
                SELECT 
                    CAST(NULLIF(dk.uuid, '') AS VARCHAR(36)) AS Uuid,
                    CAST(NULLIF(p.uuid, '') AS VARCHAR(36)) AS UuidPenelitianPkm,
                    dk.file_kontrak AS File,
                    dk.keterangan AS Keterangan
                FROM pkm_dokumen_kontrak dk 
                LEFT JOIN pkm p ON dk.id_pkm = p.id 
                WHERE dk.uuid = @Uuid and p.uuid = @UuidPenelitianPkm
                """";

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<DokumenLainnyaResponse>(sql, new { Uuid = request.Uuid, UuidPenelitianPkm = request.UuidPenelitianPkm });
            if (result == null)
            {
                return Result.Failure<DokumenLainnyaResponse>(DokumenLainnyaErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            return result;
        }
    }
}
