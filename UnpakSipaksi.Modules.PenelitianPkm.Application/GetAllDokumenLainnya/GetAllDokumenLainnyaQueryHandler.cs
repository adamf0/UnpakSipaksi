using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetDokumenLainnya;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllDokumenLainnya
{
    internal sealed class GetAllDokumenLainnyaQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllDokumenLainnyaQuery, List<DokumenLainnyaResponse>>
    {
        public async Task<Result<List<DokumenLainnyaResponse>>> Handle(GetAllDokumenLainnyaQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(dk.uuid, '') AS VARCHAR(36)) AS Uuid,
                CAST(NULLIF(p.uuid, '') AS VARCHAR(36)) AS UuidPenelitianPkm,
                dk.file_kontrak AS File,
                dk.keterangan AS Keterangan
            FROM pkm_dokumen_kontrak dk 
            LEFT JOIN pkm p ON dk.id_pkm = p.id 
            WHERE p.uuid = @UuidPenelitianPkm
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<DokumenLainnyaResponse>(sql, new { UuidPenelitianPkm = request.UuidPenelitianPkm });

            if (result == null || !result.Any())
            {
                return Result.Failure<List<DokumenLainnyaResponse>>(PenelitianPkmErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
