using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetMemberNonDosen;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetAllMemberNonDosen
{
    internal sealed class GetAllMemberNonDosenQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllMemberNonDosenQuery, List<MemberNonDosenResponse>>
    {
        public async Task<Result<List<MemberNonDosenResponse>>> Handle(GetAllMemberNonDosenQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(md.uuid, '') AS VARCHAR(36)) AS Uuid,
                CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianPkm,
                md.nomorIdentitas AS NomorIdentitas,
                md.nama AS Nama,
                md.afiliasi AS Afiliasi
            FROM pkm_anggota_non_dosen2 md 
            LEFT JOIN pkm pi ON md.id_pkm = pi.id
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<MemberNonDosenResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<MemberNonDosenResponse>>(PenelitianPkmErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
