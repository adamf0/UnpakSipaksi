using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetMemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllMemberDosen
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
                CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                md.NIDN AS NIDN,
                CASE 
                    WHEN md.status IS TRUE THEN 1
                    WHEN md.status IS FALSE THEN 0
                    ELSE NULL
                END AS Status
            FROM penelitian_internal_anggota_dosen md 
            LEFT JOIN penelitian_internal pi ON md.id_pdp = pi.id 
            WHERE pi.uuid = @UuidPenelitianHibah
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<MemberDosenResponse>(sql, new { UuidPenelitianHibah = request.UuidPenelitianHibah});

            if (result == null || !result.Any())
            {
                return Result.Failure<List<MemberDosenResponse>>(PenelitianHibahErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
