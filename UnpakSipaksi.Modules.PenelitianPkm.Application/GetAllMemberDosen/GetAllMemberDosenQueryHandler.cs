using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetMemberDosen;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllMemberDosen
{
    internal sealed class GetAllMemberDosenQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllMemberDosenQuery, List<MemberDosenResponse>>
    {
        public async Task<Result<List<MemberDosenResponse>>> Handle(GetAllMemberDosenQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(md.uuid, '') AS VARCHAR(36)) AS Uuid,
                CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianPkm,
                md.NIDN AS NIDN,
                CASE 
                    WHEN md.status IS TRUE THEN 1
                    WHEN md.status IS FALSE THEN 0
                    ELSE NULL
                END AS Status
            FROM pkm_anggota_dosen md 
            LEFT JOIN pkm pi ON md.id_pkm = pi.id 
            WHERE pi.uuid = @UuidPenelitianPkm
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<MemberDosenResponse>(sql, new { request.UuidPenelitianPkm });

            if (result == null || !result.Any())
            {
                return Result.Failure<List<MemberDosenResponse>>(PenelitianPkmErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
