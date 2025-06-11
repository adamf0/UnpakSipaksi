using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberNonDosen;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetMemberNonDosen
{
    internal sealed class GetMemberNonDosenQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMemberNonDosenQuery, MemberNonDosenResponse>
    {
        public async Task<Result<MemberNonDosenResponse>> Handle(GetMemberNonDosenQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $""""
                SELECT 
                    CAST(NULLIF(md.uuid, '') AS VARCHAR(36)) AS Uuid,
                    CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianPkm,
                    md.nomorIdentitas AS NomorIdentitas,
                    md.nama AS Nama,
                    md.afiliasi AS Afiliasi
                FROM pkm_anggota_non_dosen2 md 
                LEFT JOIN pkm pi ON md.id_pkm = pi.id
                WHERE md.uuid = @Uuid and pi.uuid = @UuidPenelitianPkm
                """";

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<MemberNonDosenResponse>(sql, new { request.Uuid, request.UuidPenelitianPkm });
            if (result == null)
            {
                return Result.Failure<MemberNonDosenResponse>(MemberNonDosenErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            return result;
        }
    }
}
