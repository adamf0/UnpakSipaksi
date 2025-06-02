using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetMemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllMemberMahasiswa
{
    internal sealed class GetAllMemberMahasiswaQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllMemberMahasiswaQuery, List<MemberMahasiswaResponse>>
    {
        public async Task<Result<List<MemberMahasiswaResponse>>> Handle(GetAllMemberMahasiswaQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(md.uuid, '') AS VARCHAR(36)) AS Uuid,
                CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                md.nim AS NPM,
                md.bukti_mbkm AS BuktiMbkm 
            FROM penelitian_internal_anggota_non_dosen md 
            LEFT JOIN penelitian_internal pi ON md.id_pdp = pi.id
            WHERE pi.uuid = @UuidPenelitianHibah
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<MemberMahasiswaResponse>(sql, new { UuidPenelitianHibah = request.UuidPenelitianHibah });

            if (result == null || !result.Any())
            {
                return Result.Failure<List<MemberMahasiswaResponse>>(PenelitianHibahErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
