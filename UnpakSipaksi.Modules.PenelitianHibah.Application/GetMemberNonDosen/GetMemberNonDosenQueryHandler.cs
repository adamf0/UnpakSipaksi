using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberNonDosen;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetMemberNonDosen
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
                    CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                    md.nomorIdentitas AS NomorIdentitas,
                    md.nama AS Nama,
                    md.afiliasi AS Afiliasi
                FROM penelitian_internal_anggota_non_dosen2 md 
                LEFT JOIN penelitian_internal pi ON md.id_pdp = pi.id
                WHERE md.uuid = @Uuid and pi.uuid = @UuidPenelitianHibah
                """";

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<MemberNonDosenResponse>(sql, new { Uuid = request.Uuid, UuidPenelitianHibah = request.UuidPenelitianHibah });
            if (result == null)
            {
                return Result.Failure<MemberNonDosenResponse>(MemberNonDosenErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            return result;
        }
    }
}
