using Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.GetKategoriProgramPengabdian;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.GetAllKategoriProgramPengabdian
{
    internal sealed class GetAllKategoriProgramPengabdianQueryHandler(IDbConnectionFactory _dbConnectionFactory) : IQueryHandler<GetAllKategoriProgramPengabdianQuery, List<KategoriProgramPengabdianResponse>>
    {
        public async Task<Result<List<KategoriProgramPengabdianResponse>>> Handle(GetAllKategoriProgramPengabdianQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await _dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            """
            SELECT 
                CAST(NULLIF(uuid, '') AS VARCHAR(36)) AS Uuid,
                 nama as Nama,
                 rule as Rule
            FROM kategori_program_pengabdian
            """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            var result = await connection.QueryAsync<KategoriProgramPengabdianResponse>(sql);

            if (result == null || !result.Any())
            {
                return Result.Failure<List<KategoriProgramPengabdianResponse>>(KategoriProgramPengabdianErrors.EmptyData());
            }

            return Result.Success(result.ToList());
        }
    }
}
