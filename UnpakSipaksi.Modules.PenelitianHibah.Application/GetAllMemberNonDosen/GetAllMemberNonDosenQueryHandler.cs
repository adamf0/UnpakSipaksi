using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetMemberNonDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Application.GetPenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetAllMemberNonDosen
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
                CAST(NULLIF(pi.uuid, '') AS VARCHAR(36)) AS UuidPenelitianHibah,
                md.nomorIdentitas AS NomorIdentitas,
                md.nama AS Nama,
                md.afiliasi AS Afiliasi
            FROM penelitian_internal_anggota_non_dosen2 md 
            LEFT JOIN penelitian_internal pi ON md.id_pdp = pi.id
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<MemberNonDosenResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<MemberNonDosenResponse>>(PenelitianHibahErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
