using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberMahasiswa;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetMemberMahasiswa
{
    internal sealed class GetMemberMahasiswaQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMemberMahasiswaQuery, MemberMahasiswaResponse>
    {
        public async Task<Result<MemberMahasiswaResponse>> Handle(GetMemberMahasiswaQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            //CAST(NULLIF(id_group, '') AS CHAR(36)) -> guid
            const string sql =
                $""""
                SELECT 
                    CAST(NULLIF(md.uuid, '') AS VARCHAR(36)) AS Uuid,
                    CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                    md.nim AS NPM,
                    md.bukti_mbkm AS BuktiMbkm 
                FROM penelitian_internal_anggota_non_dosen md 
                LEFT JOIN penelitian_internal pi ON md.id_pdp = pi.id
                WHERE md.uuid = @Uuid and pi.uuid = @UuidPenelitianHibah
                """";

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QuerySingleOrDefaultAsync<MemberMahasiswaResponse>(sql, new { Uuid = request.Uuid, UuidPenelitianHibah = request.UuidPenelitianHibah });
            if (result == null)
            {
                return Result.Failure<MemberMahasiswaResponse>(MemberMahasiswaErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            return result;
        }
    }
}
