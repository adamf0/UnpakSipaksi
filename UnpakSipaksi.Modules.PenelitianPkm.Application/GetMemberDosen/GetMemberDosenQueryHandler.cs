using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberDosen;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetMemberDosen
{
    internal sealed class GetMemberDosenQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMemberDosenQuery, MemberDosenResponse>
    {
        public async Task<Result<MemberDosenResponse>> Handle(GetMemberDosenQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $""""
                SELECT 
                    CAST(NULLIF(md.uuid, '') AS VARCHAR(36)) AS Uuid,
                    CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianPkm,
                    md.NIDN AS NIDN,
                    CASE 
                        WHEN md.status IS TRUE THEN 1
                        WHEN md.status IS FALSE THEN 0
                        ELSE NULL
                    END AS Status, 
                FROM pkm_anggota_dosen md 
                LEFT JOIN pkm pi ON md.id_pkm = pi.id
                WHERE md.uuid = @Uuid and pi.uuid = @UuidPenelitianPkm
                """";

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<MemberDosenResponse>(sql, new { request.Uuid, request.UuidPenelitianPkm });
            if (result == null)
            {
                return Result.Failure<MemberDosenResponse>(MemberDosenErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            return result;
        }
    }
}
