using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetMemberMahasiswa;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllMemberMahasiswa
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
                CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianPkm,
                md.nim AS NPM,
                md.bukti_mbkm AS BuktiMbkm 
            FROM pkm_anggota_non_dosen md 
            LEFT JOIN pkm pi ON md.id_pkm = pi.id
            WHERE pi.uuid = @UuidPenelitianPkm
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<MemberMahasiswaResponse>(sql, new { request.UuidPenelitianPkm });

            if (result == null || !result.Any())
            {
                return Result.Failure<List<MemberMahasiswaResponse>>(PenelitianPkmErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
